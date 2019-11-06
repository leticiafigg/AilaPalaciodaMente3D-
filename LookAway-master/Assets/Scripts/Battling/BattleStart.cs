using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BattleStart
{
    private Inimigo inimstats;
    private StatCalc statCalculations = new StatCalc();

    private int PlayerMaxPV;
    private int PlayerMaxPF;


    public void PrepareBattle()
    {
        EscolherOPrimeiro();  //decide quem é o primeiro a agir no combate, baseado na sorte

        DeterminarVitalidade(); // determina os pontos de vida 

    }

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
        inimstats.totalHp = statCalculations.CalcularPV(inimstats.determinacao);
        inimstats.hpatual = inimstats.totalHp;
    }

    public void EscolherOPrimeiro() 
    {
        if (GameInformation.Aila.Sorte >= inimstats.sorte)
        {
            BattleHandler.currentState = BattleHandler.BattleStates.PLAYERCHOICE;
        }
        else
        {
            BattleHandler.currentState = BattleHandler.BattleStates.ENEMYCHOICE;
        }

    }

    private void DeterminarVitalidade()
    {
        //determina a vitalidade do jogador sempre que uma batalha começa, para que esteja atualizada caso ela seja alterada por algum efeito qualquer nos status do jogador
        PlayerMaxPV = statCalculations.CalcularPV(GameInformation.Aila.Resistencia);
        PlayerMaxPF = statCalculations.CalcularPF(GameInformation.Aila.Imaginacao);
        // A vitalidade dos inimigos é determinada quando são criados, acima ^
    }


}
