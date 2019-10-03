using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleHandler : MonoBehaviour
{   
    public GameObject inimigo1obj;
    private Inimigo inim1Stats;

  
    public GameObject inimigo2obj;
    private Inimigo inim2Stats;

    private GameObject inimigodavez;

    bool xprecebido;
    public int cd; //Classe de dificuldade

    public string cenaACarregar;

    public enum BattleStates
    {
        START,
        PLAYERCHOICE,
        PLAYERANIM,
        ENEMYCHOICE,
        ENEMYANIM,
        WIN,
        LOSE

    }

    private BattleStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        xprecebido = false;
        SetEnemies();
        currentState = BattleStates.START; 
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState){

            case (BattleStates.START):
                //Apresentar os inimigos, ativa o hud e tals

               currentState = BattleStates.PLAYERCHOICE;
                break;

            case (BattleStates.PLAYERCHOICE):
                inim1Stats.agiu = false;
                inim2Stats.agiu = false;

                break;

            case (BattleStates.PLAYERANIM):

                break;

            case (BattleStates.ENEMYCHOICE):

                inimigodavez = DecidirAtor();

                if (inimigodavez != null)
                {
                    Debug.Log("Inimigo " + inimigodavez.name + " usou SPLASH!");
                    currentState = BattleStates.ENEMYANIM;
                }
                break;

            case (BattleStates.ENEMYANIM):
                
                //faz os paranaue de animar la

                if(inimigodavez = inim2Stats.inimigoobj)
                {
                    currentState = BattleStates.PLAYERCHOICE;
                }
               
                break;

            case (BattleStates.WIN):
                if (!xprecebido)
                {
                    IncreaseExperience.AddExperience(cd);
                    xprecebido = true;
                }

                SceneManager.LoadScene(cenaACarregar);

                break;

            case (BattleStates.LOSE):

                break;
        }
        


    }

    private GameObject DecidirAtor()
    {
        if (!inim1Stats.agiu && inim1Stats != null)
        {
            return inim1Stats.inimigoobj;
        }
        else if (!inim2Stats.agiu && inim2Stats != null)
        {
            return inim2Stats.inimigoobj;
        }
        else
        return null;
    }

    private void SetEnemies()
    {
        //  Instantiate(inim1, inimigo1Start.transform.position, Quaternion.identity);
        //  Instantiate(inim2, inimigo2Start.transform.position, Quaternion.identity);
        inim1Stats = inimigo1obj.GetComponent<Inimigo>();
        inim2Stats = inimigo2obj.GetComponent<Inimigo>();
    }

    public void Ataque()
    {
        if (currentState == BattleStates.PLAYERCHOICE)
        {
            Inimigo alvo;

            if (inim1Stats.hpatual >= 0)
            {
                alvo = inim1Stats;
            }
            else
            {
                alvo = inim2Stats;
            }

            alvo.TakeDamage(20);

            if(inim1Stats.derrotado && inim2Stats.derrotado)
            {
                currentState = BattleStates.WIN;

            }


            currentState = BattleStates.ENEMYCHOICE;
        }
    }

    

}
