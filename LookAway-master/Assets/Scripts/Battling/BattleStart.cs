﻿using System.Collections;
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

    public List<GameObject> PrepareEnemies(GameObject[] inims)
    {
        List<GameObject> inimigos = new List<GameObject>();
        int rdn;

        for(int i = 0 ; i < inims.Length; i++)
        {
            rdn = Random.Range(0, inims.Length);
            GameObject inimstatsObj = new GameObject();
            inimstatsObj = inims[rdn];
            
            inimstats = inimstatsObj.GetComponent<Inimigo>();
            inimstats.PosInList = i;

            CreateNewEnemy();
            //inimstats.totalHp = inimstats.Enemylvl * 10;


            inimigos.Add(inimstatsObj);
        }


        return inimigos;
    }

    

    public void CreateNewEnemy()
    {
        if(GameInformation.Aila.PlayerLevel <= inimstats.maxlvl) // caso o nível do jogador não seja maior que o nível máximo do inimigo, ele escolhe u nível aleatório.
        { 
            inimstats.EnemyLevel = Random.Range(inimstats.minlvl, inimstats.maxlvl);
        }
        else //se não, o inimigo sempre estará no nível máximo
        {
            inimstats.EnemyLevel = inimstats.maxlvl;
        }

        inimstats.poder = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.PODER, inimstats.EnemyLevel);
        inimstats.imaginacao = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.IMAGINACAO, inimstats.EnemyLevel);
        inimstats.resistencia = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.RESISTENCIA, inimstats.EnemyLevel);
        inimstats.determinacao = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.DETERMINACAO, inimstats.EnemyLevel);
        inimstats.sorte = statCalculations.CalcularInimStats(inimstats.poder, StatCalc.StatType.SORTE, inimstats.EnemyLevel);

        inimstats.pvTotal = statCalculations.CalcularPV(inimstats.resistencia);
        inimstats.pvAtual = inimstats.pvTotal;

        inimstats.stunTotal = 100;
        inimstats.stunAtual = 0;

    }

    public void EscolherOPrimeiro() //Escolhe quem age primeiro baseado no status Sorte. É chamado sempre que uma nova rodada começa para que, caso algum efeito aumente a sorte de qualquer partido, este tenha utilidade em combate
    {
        foreach (Inimigo inim in BattleHandler.inimigosList) //checa uma vez para cada inimigo na lista
        {
            if (GameInformation.Aila.Sorte * 1.5 >= inimstats.sorte)
            {
                BattleHandler.currentState = BattleHandler.BattleStates.PLAYERCHOICE;
            }
            else
            {
                BattleHandler.currentState = BattleHandler.BattleStates.ENEMYCHOICE;
            }
        }    }

    private void DeterminarVitalidade()
    {
        //determina a vitalidade máxima do jogador sempre que uma batalha começa, para que esteja atualizada caso ela seja alterada por algum efeito qualquer nos status do jogador
        PlayerMaxPV = statCalculations.CalcularPV(GameInformation.Aila.Resistencia);
        PlayerMaxPF = statCalculations.CalcularPF(GameInformation.Aila.Imaginacao);
        // A vitalidade dos inimigos é determinada quando são criados, acima ^
    }


}
