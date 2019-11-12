using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour
{

    public GameObject[] enemyFabs; // Possíveis inimigos a serem encontrados
    public GameObject playerCamera;
    public GameObject enemyActionCamera;
    private GameObject activeCamera;

    private BattleStart battleStartscript = new BattleStart();
    private BattleCalculations battleCalcScript = new BattleCalculations();
    private BaseAction baseActscript = new BaseAction();
    private BattleStateAddStatusEffect battleAddEffectscript = new BattleStateAddStatusEffect();
    private BattleStateEnemyChoice battleStateEnemyChoicescript = new BattleStateEnemyChoice();
    

    public static BaseAction playerUsedAction;
    public static BaseAction enemyUsedAction;
    public static Inimigo inimAlvo;
    public static Inimigo inimigodavez;

    public static int statusEffectBaseDamage;
    public static int totalRoundCounter; //Total de rodadas deste o primeiro turno.

    public static bool jogadorTerminouTurno;
    public static bool inimigoTerminouTurno;
    public static bool waitActive;
    

    public GameObject inimigo1start;
    public GameObject inimigo2start;
    public GameObject inimigo3start;

    public GameObject turnLogBox;
    public static string turnLogText; //alterar essa string para cada coisa que acontecer nos turnos do inimigos
    public float startWaitTime;
    private float waitTime;

    public static List<GameObject> inimObjList;
    public static List<Inimigo> inimigosList;


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
        inimigosList = new List<Inimigo>();
        inimObjList = new List<GameObject>();

        waitActive = false;
        waitTime = startWaitTime;
        xprecebido = false;
        totalRoundCounter = 1;
        SetEnemies(); //chama o PrepareEnemies do Battle Start para criá-los e então os associa pontos específicos do mapa 

        currentState = BattleStates.START; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(currentState);

        if(!waitActive) //Só lê o próximo estado estado, ou seja, só avança o game state se não tiver que esperar por algo, seja la o que for
        { 
          switch (currentState) {
          
              case (BattleStates.START):
                  //Apresentar os inimigos, ativa o hud e tals
          
                  battleStartscript.PrepareBattle();
          
                  break;
          
              case (BattleStates.PLAYERCHOICE):
                  turnLogText = "Sua vez!";
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

                    if (currentActor == BattleStates.ENEMYCHOICE)
                    {
                        battleCalcScript.CalculateTotalEnemyDMG(enemyUsedAction, inimigodavez);
                        turnLogText = inimigodavez.name + " usou " + enemyUsedAction.ActionName;
                        waitActive = true;
                    }
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

                    waitActive = true;

                  break;
          
              case (BattleStates.WIN):
          
                  //Código que mostra resultados da batalha como XP e itens aqui
          
                  if (!xprecebido)
                  {
                      IncreaseExperience.AddExperience(cd);
                      xprecebido = true;
                  }
                  GameInformation.returningFromBattle = true;
                  SceneManager.LoadScene(GameInformation.LastScene);
                  break;
          
              case (BattleStates.LOSE):
          
                  break;
          }

        }
        else
        {
            waitTime -= Time.deltaTime;

            if(waitTime <= 0)
            {
                waitActive = false;
                waitTime = startWaitTime;
            }
        }

        turnLogBox.GetComponent<TextMeshProUGUI>().text = turnLogText;
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
            foreach (Inimigo inim in inimigosList) //ao decidir que a rodada acabou cada inimigo pode agir de novo
            {
                inim.Agiu = false;
            }

        }

        if(!jogadorTerminouTurno && !inimigoTerminouTurno)
        {
           foreach(Inimigo inim in inimigosList)
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
            

            inimObjList.Add(Instantiate(inims[0], inimigo1start.transform.position, Quaternion.identity));
            inimObjList.Add(Instantiate(inims[1], inimigo2start.transform.position, Quaternion.identity));
            inimObjList.Add(Instantiate(inims[2], inimigo3start.transform.position, Quaternion.identity));

            inimigosList.Add(inimObjList[0].GetComponent<Inimigo>());
            inimigosList.Add(inimObjList[1].GetComponent<Inimigo>());
            inimigosList.Add(inimObjList[2].GetComponent<Inimigo>());


            BattleUICursor.SetCursorEnemies();
        }
        
        

    }
  
    public void Fuga()
    {
        SceneManager.LoadScene(GameInformation.LastScene);
    }
    

}
