using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart
{
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
                Inimigo inimstats = inimigos[i].GetComponent<Inimigo>();

                inimstats.Enemylvl = 2;
                inimstats.totalHp = inimstats.Enemylvl * 10;
            }
        }


        return inimigos;
    }

    public void CreateNewEnemy()
    {


    }


}
