using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleHandler : MonoBehaviour
{

    public GameObject[] enemyFabs; // Possíveis inimigos a serem encontrados
   

    private BattleStart battleStartscript = new BattleStart();
    private BattleCalculations battleCalcScript = new BattleCalculations();
    private BaseAction baseActscript = new BaseAction();

    public static BaseAction playerUsedAction;


    private GameObject inimigo1obj;   // ter até 3 inimigos, às vezes menos
    private Inimigo inim1Stats;
    public GameObject inimigo1start;
  
    private GameObject inimigo2obj;
    private Inimigo inim2Stats;
    public GameObject inimigo2start;

    private GameObject inimigo3obj;
    private Inimigo inim3Stats;
    public GameObject inimigo3start;

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
        CALCDAMAGE,
        ADDSTATUSEFFECT,
        WIN,
        LOSE

    }

    public static BattleStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        xprecebido = false;
        
        SetEnemies(); //chama o PrepareEnemies do Battle Start para criá-los e então os associa pontos específicos do mapa 

        currentState = BattleStates.START; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(currentState);

        switch(currentState){

            case (BattleStates.START):
                //Apresentar os inimigos, ativa o hud e tals

                battleStartscript.PrepareBattle();
               

                break;

            case (BattleStates.PLAYERCHOICE):
               

                break;

            case (BattleStates.PLAYERANIM):

                break;

            case (BattleStates.ENEMYCHOICE):

                /* inimigodavez = DecidirAtor();

                 if (inimigodavez != null)
                 {
                     Debug.Log("Inimigo " + inimigodavez.name + " usou SPLASH!");
                     currentState = BattleStates.ENEMYANIM;
                 }
                 else
                 {
                     currentState = BattleStates.PLAYERCHOICE;
                 }*/

                Debug.Log("Enemy choice Made");

                currentState = BattleStates.PLAYERCHOICE;

                break;

            case (BattleStates.ENEMYANIM):
                
                //faz os paranaue de animar la

               
                break;

            case (BattleStates.CALCDAMAGE):
                Debug.Log("CALCULANDO DANO");
                battleCalcScript.CalculateUsedPlayerActionDMG(playerUsedAction);
               

                break;

            case (BattleStates.ADDSTATUSEFFECT):

              //Adicionar status no alvo, se houver algum


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
            inim1Stats.agiu = true;
            return inim1Stats.inimigoobj;
        }
        else if (!inim2Stats.agiu && inim2Stats != null)
        {
            inim2Stats.agiu = true;
            return inim2Stats.inimigoobj;
        }
        else
        return null;
    }

    private void SetEnemies()
    {
        GameObject[] inims = battleStartscript.PrepareEnemies(enemyFabs);
        

        if (inims != null)
        {


            Instantiate(inims[0], inimigo1start.transform.position, Quaternion.identity);
            Instantiate(inims[1], inimigo2start.transform.position, Quaternion.identity);
            Instantiate(inims[2], inimigo3start.transform.position, Quaternion.identity);

          //  myPrefabA.transform.position = inimigo1start.transform.position;
          //  myPrefabB.transform.position = inimigo2start.transform.position;
          //  myPrefabC.transform.position = inimigo3start.transform.position;

            inim1Stats = inims[0].GetComponent<Inimigo>();
            inim2Stats = inims[1].GetComponent<Inimigo>();
            inim3Stats = inims[2].GetComponent<Inimigo>();

          
        }
        
    }

   
    /*
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
    } */

    public void Fuga()
    {

        SceneManager.LoadScene(GameInformation.LastScene);

    }
    

}
