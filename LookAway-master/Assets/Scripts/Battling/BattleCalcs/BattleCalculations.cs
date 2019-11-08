using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculations 
{
    private StatCalc statCalcScript = new StatCalc();

    private BaseAction playerusedAction;

    private int actionPower;
    private int statusEffDmg;
    private int stunPower = 0;
    
    

    private int totalActionDMG;
    private int totalCriticalDMG;
    private int totalEffectDMG;
    private float totalPlayerDMG; //agrega o valor de dano da ação com o efeito, só para deixar separado
    private int totalStunDMG; //stun é mais simples

    private float dmgVariator = 0.025f; // 5%

    public void CalculateTotalPlayerDMG(BaseAction usedAction)
    {
        playerusedAction = usedAction;
        Debug.Log("Aila usou " + usedAction.ActionName);
        
        totalActionDMG = (int)CalculateActionDMG();
        totalCriticalDMG = CalculateCriticalDMG();
        totalEffectDMG = CalculateStatusEffectDMG();

        totalPlayerDMG = totalActionDMG + totalCriticalDMG + totalEffectDMG; //Dano combinado do ataque em si, + o crítico, mais o status.

        totalPlayerDMG += (int)(Random.Range(-(totalPlayerDMG * dmgVariator), totalPlayerDMG * dmgVariator)); // adiciona uma variaçãod e 5% entre danos, afinal raramente um ataque de uma mesma pessoa causa exatamente o mesmo dano 
        Debug.Log("Causou " + totalPlayerDMG + " de dano total com o efeito");
        BattleHandler.jogadorTerminouTurno = true;

        BattleHandler.currentState = BattleHandler.BattleStates.ENEMYCHOICE;

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
            Debug.Log("Uau! Um golpe crítico!");
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


}
