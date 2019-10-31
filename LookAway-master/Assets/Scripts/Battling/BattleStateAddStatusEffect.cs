using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateAddStatusEffect 
{
   public void CheckActionStatus(BaseAction usedAction)
    {
        //checa se há algum efeito no ataque usado
        if (usedAction.ActionEffects.Count > 0) //Se o item na posição zero for diferente de nulo, então há algum efeito na habilidade (Pelo menos 1)
        {
            Debug.Log("Tem o status: " + usedAction.ActionEffects[0].StatusEffectName);
        }
        else
        {
            Debug.Log("Não adiciona status");
        }

        BattleHandler.currentState = BattleHandler.BattleStates.ENEMYCHOICE;
    }
   
}
