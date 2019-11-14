using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyChoice 
{
    private EnemyActionChoice enemyActionChooseScript = new EnemyActionChoice();
    public BaseAction acaoDoInimigo;
    //private int inimIndex;


    public void EnemyCompleteTurn(int inimIndex)
    {     

        if (BattleHandler.inimigosList.Count > 0)
        {
            //escolher uma ação
            if (inimIndex < BattleHandler.inimigosList.Count)
            {
                BattleHandler.inimigodavez = BattleHandler.inimigosList[inimIndex]; //Salva o inimigo que vai agir em "inimigodavez" do BattleHandler, o script central, que vai por sua vez mandar os status do inimigo junto com a ação escolhida 

                
                    if (!BattleHandler.inimigodavez.Atordoado) //Apenas vai escolher uma ação se não estiver atordoado. Se estiver, não faz nada
                    {
                        acaoDoInimigo = enemyActionChooseScript.ChooseEnemyAction(BattleHandler.inimigodavez);
                        BattleHandler.enemyUsedAction = acaoDoInimigo;

                    }
                    else
                    {

                        BattleHandler.inimigodavez.Atordoado = false;
                        BattleHandler.inimigodavez.stunAtual = 0;
                        BattleHandler.enemyUsedAction = new Zonzar();
                    }
                
        
                BattleHandler.inimigodavez.Agiu = true;

            }
        }

             //Atualizar a ação feita no text log e pausar o game state para então quando o gamestate voltar a rodar ele ler o CALCDAMAGE, que diz o dano causado
             BattleHandler.waitActive = true;
             BattleHandler.turnLogText = BattleHandler.inimigodavez.Nome + " usou " + BattleHandler.enemyUsedAction.ActionName; 

             BattleHandler.currentState = BattleHandler.BattleStates.CALCDAMAGE;              
    }

}
