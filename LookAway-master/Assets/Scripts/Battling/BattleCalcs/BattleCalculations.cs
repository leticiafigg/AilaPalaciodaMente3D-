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
    
    

    private int totalActionDMG;
    private int totalCriticalDMG;
    private int totalEffectDMG;
    private float totalPlayerDMG; //agrega o valor de dano da ação com o efeito, só para deixar separado
    private int totalStunDMG; //stun é mais simples

    private float totalEnemyDMG;//segue regras semelhantes ao jogador, porém com algumas alterações

    private float dmgVariator = 0.025f; // 5%

    public void CalculateTotalPlayerDMG(BaseAction usedAction, Inimigo inimAlvo)
    {
        playerusedAction = usedAction;
        Debug.Log("Aila usou " + usedAction.ActionName);
        
        totalActionDMG = (int)CalculateActionDMG();
        totalCriticalDMG = CalculateCriticalDMG();
        totalEffectDMG = CalculateStatusEffectDMG();

        totalPlayerDMG = totalActionDMG + totalCriticalDMG + totalEffectDMG; //Dano combinado do ataque em si, + o crítico, mais o status.

        totalPlayerDMG += (int)(Random.Range(-(totalPlayerDMG * dmgVariator), totalPlayerDMG * dmgVariator)); // adiciona uma variaçãod e 5% entre danos, afinal raramente um ataque de uma mesma pessoa causa exatamente o mesmo dano 
        Debug.Log("Aila causou " + totalPlayerDMG + " de dano total com o efeito");

        totalPlayerDMG = calculateEnemyResistance(inimAlvo);

        inimAlvo.TakeDamage((int)totalPlayerDMG); //Chama o método de tomar dnao dentro do script do inimigo alvo

        BattleHandler.jogadorTerminouTurno = true;
    }

    public void CalculateTotalEnemyDMG(BaseAction usedAction , Inimigo inim)
    {
        inimigoAgindo = inim;
        enemyusedAction = usedAction;

        totalActionDMG = (int)CalculateEnemyActionDMG();
        totalCriticalDMG = CalculateEnemyCriticalDMG();
        totalEffectDMG = CalculateStatusEffectDMG();

        totalEnemyDMG = totalActionDMG + totalCriticalDMG + totalEffectDMG;

        if(DecidirEvasion())
        {
            totalEnemyDMG = 0;
            Debug.Log("Desviou!");
        }

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
        totalActionPowerDMG = actionPower * (statCalcScript.GetEnemyActionAffinity(enemyusedAction.StatAffinity, inimigoAgindo));

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
            criticalDMG = (int)(totalActionDMG + (totalActionDMG * (0.1f + (inimigoAgindo.determinacao * 0.1f))));
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
            return true;
        }
        return false;
    }

    private bool DecidirEnemyActionCriticalHit()
    {
        int randomTemp = Random.Range(0, 100);

        //chance alterada pelo valor de Sorte do jogador = 10% da sorte;
        int chanceModificada = (int)(enemyusedAction.ActionCritChance + (inimigoAgindo.sorte * 0.1f));

        if (randomTemp <= chanceModificada)
        {
            return true;
        }
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
}
