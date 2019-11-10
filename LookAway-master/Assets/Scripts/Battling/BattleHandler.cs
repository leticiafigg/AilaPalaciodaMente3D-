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
    private BattleStateAddStatusEffect battleAddEffectscript = new BattleStateAddStatusEffect();
    private BattleStateEnemyChoice battleStateEnemyChoicescript = new BattleStateEnemyChoice();

    public static BaseAction playerUsedAction;
    public static BaseAction enemyUsedAction;
    public static Inimigo inimAlvo;
    public static Inimigo inimigodavez;
    public static List<Inimigo> inimStatsList;

    public static int statusEffectBaseDamage;
    public static int totalRoundCounter; //Total de rodadas deste o primeiro turno.

    public static bool jogadorTerminouTurno;
    public static bool inimigoTerminouTurno;

    private GameObject inimigo1obj;   // ter até 3 inimigos, às vezes menos
    private Inimigo inim1Stats;
    public GameObject inimigo1start;
  
    private GameObject inimigo2obj;
    private Inimigo inim2Stats;
    public GameObject inimigo2start;

    private GameObject inimigo3obj;
    private Inimigo inim3Stats;
    public GameObject inimigo3start;


    

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
        ENDROUND,
        CALCDAMAGE,
        ADDSTATUSEFFECT,
        WIN,
        LOSE

    }

    public static BattleStates currentState;
    public static BattleStates currentActor; //uma instância de BattleStates apenas para armazenar quem está agindo
    // Start is called before the first frame update
    void Start()
    {
        inimStatsList = new List<Inimigo>(0);
        
        xprecebido = false;
        totalRoundCounter = 1;
        SetEnemies(); //chama o PrepareEnemies do Battle Start para criá-los e então os associa pontos específicos do mapa 

        currentState = BattleStates.START; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(currentState);

        switch (currentState) {

            case (BattleStates.START):
                //Apresentar os inimigos, ativa o hud e tals

                battleStartscript.PrepareBattle();

                break;

            case (BattleStates.PLAYERCHOICE):
                currentActor = BattleStates.PLAYERCHOICE; //armazenando que o ator é o jogador
                break;

            case (BattleStates.PLAYERANIM):

                break;

            case (BattleStates.ENEMYCHOICE):
                //colocar IA aqui
                currentActor = BattleStates.ENEMYCHOICE;

                battleStateEnemyChoicescript.EnemyCompleteTurn();
                
                //DecidirProximoAtor();
                break;

            case (BattleStates.ENEMYANIM):
                //faz os paranaue de animar la
                break;

            case (BattleStates.CALCDAMAGE):
                Debug.Log("CALCULANDO DANO");
                if(currentActor == BattleStates.PLAYERCHOICE ) //se é o turno do jogador e ele escolheu alguma ação
                battleCalcScript.CalculateTotalPlayerDMG(playerUsedAction , inimAlvo);

                if(currentActor == BattleStates.ENEMYCHOICE)
                battleCalcScript.CalculateTotalEnemyDMG(enemyUsedAction , inimigodavez);

                DecidirProximoAtor();
                break;

            case (BattleStates.ADDSTATUSEFFECT):
                //Adicionar status no alvo, se houver algum
                battleAddEffectscript.CheckActionStatus(playerUsedAction);
                break;

            case (BattleStates.ENDROUND):
                totalRoundCounter += 1;

              
                jogadorTerminouTurno = false;
                inimigoTerminouTurno = false;
                DecidirProximoAtor();
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

    private void DecidirProximoAtor()
    {
        if(jogadorTerminouTurno && !inimigoTerminouTurno)
        {
            //Vez do inimigo
            currentState = BattleStates.ENEMYCHOICE;
        }
        if(!jogadorTerminouTurno && inimigoTerminouTurno)
        {
            //vez do jogador
            currentState = BattleStates.PLAYERCHOICE;
        }
        if(jogadorTerminouTurno && inimigoTerminouTurno)
        {
            //terminar a rodada
            currentState = BattleStates.ENDROUND;
            foreach (Inimigo inim in inimStatsList) //ao decidir que a rodada acabou cada inimigo pode agir de novo
            {
                inim.Agiu = false;
            }

        }

        if(!jogadorTerminouTurno && !inimigoTerminouTurno)
        {
           foreach(Inimigo inim in inimStatsList)
            {
                if(GameInformation.Aila.Sorte >= inim.sorte)
                {
                    currentState = BattleStates.PLAYERCHOICE;
                }
                else
                {
                    currentState = BattleStates.ENEMYCHOICE;
                }

            }

        }

    }

    private void SetEnemies()
    {
        GameObject[] inims = battleStartscript.PrepareEnemies(enemyFabs);
        

        if (inims != null)
        {
            GameObject[] inimSave= new GameObject[3];

            inimSave[0] = Instantiate(inims[0], inimigo1start.transform.position, Quaternion.identity);
            inimSave[1] = Instantiate(inims[1], inimigo2start.transform.position, Quaternion.identity);
            inimSave[2] = Instantiate(inims[2], inimigo3start.transform.position, Quaternion.identity);

            inim1Stats = inimSave[0].GetComponent<Inimigo>();
            inim2Stats = inimSave[1].GetComponent<Inimigo>();
            inim3Stats = inimSave[2].GetComponent<Inimigo>();

            inimStatsList.Add(inim1Stats);
            inimStatsList.Add(inim2Stats);
            inimStatsList.Add(inim3Stats);

            BattleUICursor.SetCursorEnemies(inimSave);
        }
        
        

    }
  
    public void Fuga()
    {
        SceneManager.LoadScene(GameInformation.LastScene);
    }
    

}
