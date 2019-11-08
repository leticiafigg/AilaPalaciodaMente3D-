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
    private float totalActionPowerDMG;
    

    private int totalActionDMG;
    private float totalPlayerDMG; //agrega o valor de dano da ação com o efeito, só para deixar separado
    private int totalStunDMG; //stun é mais simples

    private float dmgVariator = 0.05f; // 5%

    public void CalculateTotalPlayerDMG(BaseAction usedAction)
    {
        playerusedAction = usedAction;
        Debug.Log("Aila usou " + usedAction.ActionName);
        //action dmg + crit - armor + statmod + equip
        totalActionDMG = (int)CalculateActionDMG();

        if (DecidirActionCriticalHit())
        {
            totalActionDMG = CalculateCriticalDMG();
            totalStunDMG = totalStunDMG * 2; //Stun sempre é dobrado.
            Debug.Log("Uau! Um golpe crítico!");
        }

        

        totalPlayerDMG = totalActionDMG + CalculateStatusEffectDMG();

        totalPlayerDMG += (int)(Random.Range(-(totalPlayerDMG * dmgVariator), totalPlayerDMG * dmgVariator)); // adiciona uma variaçãod e 5% entre danos, afinal raramente um ataque de uma mesma pessoa causa exatamente o mesmo dano 
        Debug.Log("Causou " + totalPlayerDMG + " de dano total com o efeito");

        BattleHandler.currentState = BattleHandler.BattleStates.ENEMYCHOICE;

    }

    public void CalculateUsedPlayerActionDMG()
    {
       
        //usar uma ação
        //calcular o dano
        //checa status
          //se tiver um status
              //tentar acionar o feito
              
        //calcular total de dano com status juntos
        
    }

    private float CalculateActionDMG()
    {
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
              //O crítico causa 100% + uma porcentagem retirada da determinação de dano extra
        criticalDMG = (int)(totalActionDMG + totalActionDMG + (totalActionDMG * (0.1f + (GameInformation.Aila.Determinacao * 0.1f) ) ) );

        return criticalDMG;
    }

    private bool DecidirActionCriticalHit()
    {
        int randomTemp = Random.Range(0, 100);
        if(randomTemp <= playerusedAction.ActionCritChance)
        {
            return true;
        }
        return false;
    }


}
