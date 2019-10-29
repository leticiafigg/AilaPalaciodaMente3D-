using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculations 
{
    private StatCalc statCalcScript = new StatCalc();


    private int actionPower;
    private int stunPower = 0;
    private float totalActionPowerDMG;
    
    private int totalActionDMG;
    private int totalStunDMG; //stun é mais simples


    public void CalculateUsedPlayerActionDMG(BaseAction usedAction)
    {
        Debug.Log("Aila usou " + usedAction.ActionName);
        //action dmg + crit - armor + statmod + equip
        totalActionDMG = (int)CalculateActionDMG(usedAction);
        Debug.Log("Causou " + totalActionDMG + " de dano");
        //usar uma ação
        //calcular o dano
        //checa status
          //se tiver um status
              //tentar acionar o feito
              
        //calcular total de dano com status juntos
        
    }

    private float CalculateActionDMG(BaseAction usedAction)
    {
        actionPower = usedAction.ActionPower;
        stunPower = usedAction.StunPower;
       
        totalActionPowerDMG = actionPower * statCalcScript.GetActionAffinity(usedAction.StatAffinity);

        totalStunDMG = usedAction.StunPower;

        return totalActionPowerDMG;
    }

}
