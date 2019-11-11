using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateAddStatusEffect 
{
    int statusPos;

   public void CheckActionStatus(BaseAction usedAction)
    {
        //checa se há algum efeito no ataque usado
        if (usedAction.ActionEffects.Count > 0) 
        {
           foreach (BaseStatusEffect statusDeste in usedAction.ActionEffects) // para cada status em um unico ataque, ele vai checar qual é, e se ele foi bem sucedido
            {
                statusPos = usedAction.ActionEffects.IndexOf(statusDeste); //adquire a posição do status na lista, para fazer a checagem individual no método " TentarAplicarStatus "
                switch (statusDeste.StatusEffectName)
                {

                    case("Queimadura"):
                        Debug.Log("Tentando aplicar status com " + statusDeste.StatusEffectChance + "% de chance");

                        if (TentarAplicarStatus(usedAction))
                        {
                            BattleHandler.statusEffectBaseDamage = statusDeste.StatusEffectPower;
                        }
                        else
                        {
                            BattleHandler.statusEffectBaseDamage = 0;
                        }

                        BattleHandler.currentState = BattleHandler.BattleStates.CALCDAMAGE;

                    break;

                    case ("Sono"):
                        Debug.Log("Tentando aplicar status com " + statusDeste.StatusEffectChance + "% de chance");

                        if (TentarAplicarStatus(usedAction))
                        {
                            BattleHandler.statusEffectBaseDamage = statusDeste.StatusEffectPower;
                        }
                        else
                        {
                            BattleHandler.statusEffectBaseDamage = 0;
                        }

                        
                        BattleHandler.currentState = BattleHandler.BattleStates.CALCDAMAGE;

                    break;
                }
            }
        }
        else
        {
            Debug.Log("Não adiciona status");
            BattleHandler.statusEffectBaseDamage = 0;
            BattleHandler.currentState = BattleHandler.BattleStates.CALCDAMAGE;
        }

        
    }
   
    private bool TentarAplicarStatus(BaseAction usedAction)
    {
        //ver a chance de aplicar e alterar de acordo com a afinidade
        int randomTemp = Random.Range(0, 100); // numero aleatório entre 0 e 100 para representar a porcentagem

        Debug.Log("Rolou " + randomTemp);

        if (randomTemp <= usedAction.ActionEffects[statusPos].StatusEffectChance) // se sim, aplicar o efeito
        {
            Debug.Log("Funcionou, retornando true");
            return true;
        }

        return false;
    }

}
