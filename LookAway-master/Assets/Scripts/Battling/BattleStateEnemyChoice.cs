using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyChoice 
{
    private EnemyActionChoice enemyActionChooseScript = new EnemyActionChoice();
    public BaseAction acaoDoInimigo;
    private int inimIndex;

    


    public void EnemyCompleteTurn(List<Inimigo> inimList)
    {
        inimIndex = 0; //sempre reseta para o primeiro, porém ...

        foreach (Inimigo inimstat in inimList)
        {
            if (inimstat.agiu) 
            {
                inimIndex++; //...sempre que encontra um inimigo que já agiu ele adiciona 1 no indexador
                BattleHandler.inimigoTerminouTurno = true;
            }
            else
            {
                
                //e quando encontra um que não agiu, significa que o turno dos inimigos ainda não acabou
                BattleHandler.inimigoTerminouTurno = false;
            }
        }
        //escolher uma ação
        if (inimIndex < inimList.Count)
        {
            BattleHandler.inimigodavez = inimList[inimIndex]; //Salva o inimigo que vai agir em "inimigodavez" do BattleHandler, o script central, que vai por sua vez mandar os status do inimigo junto com a ação escolhida 

            if (!BattleHandler.inimigodavez.atordoado) //Apenas vai escolher uma ação se não estiver atordoado. Se estiver, não faz nada
            {
                acaoDoInimigo = enemyActionChooseScript.ChooseEnemyAction(BattleHandler.inimigodavez);
                BattleHandler.enemyUsedAction = acaoDoInimigo;
            }
        }
        //calcular dano
        BattleHandler.currentState = BattleHandler.BattleStates.CALCDAMAGE;
        //fim de turno

        
    }

}
