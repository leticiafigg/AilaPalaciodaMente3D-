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
    public Camera playerCamera;
    public Camera enemyActionCamera;
    //private Camera activeCamera;

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
        Cursor.lockState = CursorLockMode.None;

        playerCamera.enabled = true;
        enemyActionCamera.enabled = false;

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
                    //A CD aumenta para cada inimigo na lista. por enquanto sempre vai ser 3
                    cd = inimigosList.Count;
                    turnLogText = "Falsas memórias foram encontradas!";
                    waitActive = true;
                  break;
          
              case (BattleStates.PLAYERCHOICE):
                  CameraParaJogador();
                  turnLogText = "Sua vez!";
                  currentActor = BattleStates.PLAYERCHOICE; //armazenando que o ator é o jogador
                  break;
          
              case (BattleStates.PLAYERANIM):
          
                  break;
          
              case (BattleStates.ENEMYCHOICE):
                  //colocar IA aqui
                  currentActor = BattleStates.ENEMYCHOICE;

                    //checa cada inimigo na lista para ver se ele já agiu
                    int inimIndex = 0; //sempre reseta para o primeiro, porém ...
                    foreach (Inimigo inimstat in inimigosList)
                    {
                        if (inimstat.Agiu)
                        {
                            inimIndex++; //...sempre que encontra um inimigo que já agiu ele adiciona 1 no indexador
                           inimigoTerminouTurno = true;
                        }
                        else
                        {
                            //e quando encontra um que não agiu, significa que o turno dos inimigos ainda não acabou
                           inimigoTerminouTurno = false;
                        }
                    }

                    if (inimigoTerminouTurno == false) //só entra aqui se cada inimigo na lista ainda não agiu
                    {
                        battleStateEnemyChoicescript.EnemyCompleteTurn(inimIndex);
                        enemyActionCamera.transform.position = inimigodavez.cameraPos.transform.position;
                        CameraParaInimigo();
                    }
                    else
                    {
                        DecidirProximoAtor();
                    }
                    
                    break;
          
              case (BattleStates.ENEMYANIM):
                  //faz os paranaue de animar la
                  break;
          
              case (BattleStates.CALCDAMAGE):
                  Debug.Log("CALCULANDO DANO");
                    if (currentActor == BattleStates.PLAYERCHOICE) //se é o turno do jogador e ele escolheu alguma ação
                    {
                        battleCalcScript.CalculateTotalPlayerDMG(playerUsedAction, inimAlvo);             
                    }

                    if (currentActor == BattleStates.ENEMYCHOICE && inimigodavez != null) //calcula o dano se o inimigo ainda não agiu
                    { 
                        battleCalcScript.CalculateTotalEnemyDMG(enemyUsedAction, inimigodavez);  
                        
                        inimigodavez.Agiu = true;
                    }
                  DecidirProximoAtor(); // Depois de calcular todo o dano, vai retornar para o turno dos inimigos se algum deles não terminou o turno;
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
                  CameraParaJogador();
                  turnLogText = "Fim da rodada";
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

    private void CameraParaJogador() //desativa a camera do inimigo e ativa a camera do jogador
    {
        playerCamera.enabled = true;
        enemyActionCamera.enabled = false;
    }

    private void CameraParaInimigo() //desativa a camera do inimigo e ativa a camera do jogador
    {
        playerCamera.enabled = false;
        enemyActionCamera.enabled = true;

    }

    private void DecidirProximoAtor()
    {
        if(jogadorTerminouTurno && !inimigoTerminouTurno) //Se o jogador terminou  turno, mas o inimigo não... 
        {
            //..vez do inimigo
            currentState = BattleStates.ENEMYCHOICE;          
        }
        if(!jogadorTerminouTurno && inimigoTerminouTurno) //Se o jogador não terminou o turno, mas o inimigo sim...
        {
            //...vez do jogador
            currentState = BattleStates.PLAYERCHOICE;           
        }
        if(jogadorTerminouTurno && inimigoTerminouTurno) //Se tanto o jogador quanto o inimigo terminaram seus turnos
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
            battleStartscript.EscolherOPrimeiro();
        }

    }

    private void SetEnemies() //chama o método que cria os inimigos no Script BattleStart e então os armazena na lista de objetos e na lista de inimigos 
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
