using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleStart
{
    private Inimigo inimstats;
    private StatCalc statCalculations = new StatCalc();

    private int PlayerMaxPV;
    private int PlayerMaxPF;


    public GameObject[] PrepareEnemies(GameObject[] inims)
    {
        GameObject[] inimigos = new GameObject[3];
        int rdn;

        for(int i = 0 ; i < 3; i++)
        {
            rdn = Random.Range(0, inims.Length);
            inimigos[i] = inims[rdn];

            if(inimigos[i].GetComponent<Inimigo>() != null)
            {
                inimstats = inimigos[i].GetComponent<Inimigo>();
                
                CreateNewEnemy();
                
                
                //inimstats.totalHp = inimstats.Enemylvl * 10;
            }
        }


        return inimigos;
    }



    public void CreateNewEnemy()
    {
        if(GameInformation.Aila.PlayerLevel <= inimstats.maxlvl) // caso o nível do jogador não seja maior que o nível máximo do inimigo, ele escolhe u nível aleatório.
        { 
         inimstats.EnemyLevel = Random.Range(GameInformation.Aila.PlayerLevel - 2, inimstats.maxlvl);
        }
        else //se não, o inimigo sempre estará no nível máximo
        {
            inimstats.EnemyLevel = inimstats.maxlvl;
        }

        inimstats.poder = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.PODER, inimstats.EnemyLevel);
        inimstats.imaginacao = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.IMAGINACAO, inimstats.EnemyLevel);
        inimstats.determinacao = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.DETERMINACAO, inimstats.EnemyLevel);
        inimstats.sorte = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.SORTE, inimstats.EnemyLevel);
    }

    private void DeterminarVitalidade()
    {
        PlayerMaxPV = statCalculations.CalcularPV(GameInformation.Aila.Determinacao);
        PlayerMaxPF = statCalculations.CalcularPF(GameInformation.Aila.Imaginacao);
    }


}
