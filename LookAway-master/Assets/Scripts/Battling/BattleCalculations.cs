using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleCalculations 
{
    private StatCalc statCalcScript = new StatCalc();

    private BaseAction playerusedAction;
    private BaseAction enemyusedAction;
    private Inimigo inimigoAgindo;

    private int actionPower;
    private int statusEffDmg;
    private int stunPower = 0;

    private bool foicritico; //serve apenas para dizer ao jogador se o ataque dele foi crítico ou não
    private bool foicriticoInim;

    private int totalActionDMG;
    private int totalCriticalDMG;
    private int totalEffectDMG;
    private float totalPlayerDMG; //agrega o valor de dano da ação com o efeito, só para deixar separado
    private int totalStunDMG; //stun é mais simples

    private float totalEnemyDMG;//segue regras semelhantes ao jogador, porém com algumas alterações

    private float dmgVariator = 0.05f; // 5%

    public void CalculateTotalPlayerDMG(BaseAction usedAction, Inimigo inimAlvo)
    {
        playerusedAction = usedAction;
        Debug.Log("Aila usou " + usedAction.ActionName);
        
        totalActionDMG = (int)CalculateActionDMG();
        totalCriticalDMG = CalculateCriticalDMG();
        totalEffectDMG = CalculateStatusEffectDMG();

        totalPlayerDMG = totalActionDMG + totalCriticalDMG + totalEffectDMG; //Dano combinado do ataque em si, + o crítico, mais o status.

        totalPlayerDMG += (int)(Random.Range(-(totalPlayerDMG * dmgVariator), totalPlayerDMG * dmgVariator)); // adiciona uma variaçãod e 5% entre danos, afinal raramente um ataque de uma mesma pessoa causa exatamente o mesmo dano 

       

        totalPlayerDMG = calculateEnemyResistance(inimAlvo);

        inimAlvo.TakeDamage((int)totalPlayerDMG , totalStunDMG); //Chama o método de tomar dano dentro do script do inimigo alvo

        BattleHandler.jogadorTerminouTurno = true;
        if (foicritico)
        {
            BattleHandler.turnLogText = "Uau! Um golpe crítico! Causou " + totalPlayerDMG + " de dano!";
        }
        else
        {
            BattleHandler.turnLogText = "Causou " + totalPlayerDMG + " de dano";
        }
        BattleHandler.waitActive = true;
    }

    public void CalculateTotalEnemyDMG(BaseAction usedAction , Inimigo inim)
    {
        inimigoAgindo = inim;
        enemyusedAction = usedAction;

        totalActionDMG = (int)CalculateEnemyActionDMG();
        totalCriticalDMG = CalculateEnemyCriticalDMG();
        totalEffectDMG = CalculateStatusEffectDMG();

        totalEnemyDMG = totalActionDMG + totalCriticalDMG + totalEffectDMG;

        totalEnemyDMG = calculatePlayerResistance();

        totalEnemyDMG += (int)(Random.Range(-(totalEnemyDMG * dmgVariator), totalEnemyDMG * dmgVariator));

        if (DecidirEvasion()) //se Aila desviou...
        {
            totalEnemyDMG = 0;
            BattleHandler.turnLogText = "Aila desviou do ataque";
        }
        else if (foicriticoInim) //se não desviou, e na verdade tomou um crítico.... 
        {
            BattleHandler.turnLogText = "Ouch! um crítico inimigo! Causou " + totalEnemyDMG + " de dano!";
        }
        else //se nenhum destes ocorreu, então foi dano normal
        {
            BattleHandler.turnLogText = "Causou " + totalEnemyDMG + " de dano";
        }

        BattleHandler.waitActive = true;

        GameInformation.AilaPVatual -= (int)totalEnemyDMG;

    }

    private float CalculateActionDMG()
    {
        float totalActionPowerDMG;


        actionPower = playerusedAction.ActionPower;
        stunPower = playerusedAction.StunPower;
       
        //ações possuem uma afinidade com algum status, o qual torna o movimento mais poderoso quanto maior for o valor
        totalActionPowerDMG = actionPower * (statCalcScript.GetActionAffinity(playerusedAction.StatAffinity)); 

        totalStunDMG = playerusedAction.StunPower;

        return totalActionPowerDMG;
    }

    private float CalculateEnemyActionDMG()
    {
        float totalActionPowerDMG;


        actionPower = enemyusedAction.ActionPower;
        stunPower = 0;

        //ações possuem uma afinidade com algum status, o qual torna o movimento mais poderoso quanto maior for o valor
        totalActionPowerDMG = actionPower * (statCalcScript.GetEnemyActionAffinity(enemyusedAction.StatAffinity, inimigoAgindo) * 0.8f);

        totalStunDMG = 0;

        return totalActionPowerDMG;
    }

    private int CalculateStatusEffectDMG()
    {
        statusEffDmg = BattleHandler.statusEffectBaseDamage * (int)(GameInformation.Aila.Imaginacao * 0.25f);
        Debug.Log("O dano de status causado é: " + statusEffDmg);
        return statusEffDmg;
    }

    private int CalculateCriticalDMG()
    {
        int criticalDMG;

        if(DecidirActionCriticalHit())
        {
            //Crítico adiciona dano ao dano total = 100% + uma porcentagem retirada da determinação de dano extra
            criticalDMG = (int)(totalActionDMG + (totalActionDMG * (0.1f + (GameInformation.Aila.Determinacao * 0.1f))));
            totalStunDMG = totalStunDMG * 2; //Stun sempre é dobrado.
            Debug.Log("Uau! Um ataque crítico!");
            return criticalDMG;
        }

        return 0;
       
    }

    private int CalculateEnemyCriticalDMG()
    {
        int criticalDMG;

        if (DecidirEnemyActionCriticalHit())
        {
            //Crítico adiciona dano ao dano total = 100% + uma porcentagem retirada da determinação de dano extra
            criticalDMG = (int)(totalActionDMG + totalActionDMG);
            totalStunDMG = totalStunDMG * 2; //Stun sempre é dobrado.
            Debug.Log("Ouch! Um golpe crítico do inimigo!");
            return criticalDMG;
        }

        return 0;
    }

    private bool DecidirActionCriticalHit()
    {
        int randomTemp = Random.Range(0, 100);

        //chance alterada pelo valor de Sorte do jogador = 10% da sorte;
        int chanceModificada = (int)(playerusedAction.ActionCritChance + (GameInformation.Aila.Sorte * 0.1f));

        if(randomTemp <= chanceModificada)
        {
            foicritico = true;
            return true;
        }

        foicritico = false;
        return false;
    }

    private bool DecidirEnemyActionCriticalHit()
    {
        int randomTemp = Random.Range(0, 100);

        //chance alterada pelo valor de Sorte do jogador = 10% da sorte;
        int chanceModificada = (int)(enemyusedAction.ActionCritChance + (inimigoAgindo.sorte * 0.1f));

        if (randomTemp <= chanceModificada)
        {
            foicriticoInim = true;
            return true;
        }

        foicriticoInim = false;

        return false;
    }

    private bool DecidirEvasion()
    {
        int randomTemp = Random.Range(0, 100);

        int chanceEvasModificada = (int)(10 + +(GameInformation.Aila.Sorte * 0.2f));

        if(randomTemp + inimigoAgindo.determinacao *0.1 + inimigoAgindo.poder * 0.1 < chanceEvasModificada)
        {
            return true;
        }

        return false;
    }

   private float calculateEnemyResistance(Inimigo inim)
   {
        float resistedDMG = 0.0f;

        resistedDMG = totalPlayerDMG - (int)((inim.determinacao * 0.25) + (inim.resistencia * 0.50 + inim.armadura));
        Debug.Log("Dano total  depois da defesa " + resistedDMG);
        return resistedDMG;
    
   }

    private float calculatePlayerResistance()
    {
        float resistedDMG = 0.0f;

        resistedDMG = totalEnemyDMG - (int)((GameInformation.Aila.Determinacao * 0.25) + (GameInformation.Aila.Resistencia * 0.5) + GameInformation.Aila.Armadura);
        Debug.Log("Dano que o jogador recebeu depois da defesa " + resistedDMG);

        if (resistedDMG <= 0)
            resistedDMG = 0;

        return resistedDMG;

    }
}
