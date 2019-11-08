using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyChoice 
{
    private EnemyActionChoice enemyActionChooseScript = new EnemyActionChoice();
    public BaseAction acaoDoInimigo;
    private int inimIndex;

    private Inimigo inimigodavez;


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
            inimigodavez = inimList[inimIndex];

            if (!inimigodavez.atordoado)
            {
                acaoDoInimigo = enemyActionChooseScript.ChooseEnemyAction(inimigodavez);
            }
        }
        //calcular dano
        //fim de turno

        
    }

}
