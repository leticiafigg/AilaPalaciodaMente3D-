using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleUI : MonoBehaviour
{
    //Ernergia, recurso usado para algumas ações
    private BaseAction standbyAction;
    private bool pressedBtn;
    private KeyCode pressedKey;

    public static Inimigo inimigoAlvo;

    public GameObject panelActions;
    public GameObject panelConfirmAttack;
    public BattleUICursor cursorUI;
    public int adjustX = 0;
    public int adjustY = 0;

    private enum ScreenDisplays
    {
        NEUTRALDISPLAY,
        ATTACKSDISPLAY,
        FANTASYDISPLAY,
        TARGETDISPLAY,  //Não apenas inicia o hud de escolha, como também exibe a descrição da ação selecionada
        ITEMDISPLAY,

    }

    private ScreenDisplays currentDisplay;


    // Start is called before the first frame update
    void Start()
    {
        //playerName = GameInformation.Aila.PlayerName;
        // playerLvl = GameInformation.Aila.PlayerLevel;
        pressedBtn = false;
        currentDisplay = ScreenDisplays.NEUTRALDISPLAY;
        //playerPV 
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        switch(currentDisplay)
        {
            case (ScreenDisplays.NEUTRALDISPLAY):

                NeutralDisplayHandle();

                break;

            case (ScreenDisplays.TARGETDISPLAY):

                TargetDisplayHandle();

                break;

        }
    }

    private void NeutralDisplayHandle()
    {
        if (BattleHandler.currentState == BattleHandler.BattleStates.PLAYERCHOICE) //caso o display seja do menu neutro, *E* estamos no estado "escolha do jogador" ativa o painel neutro
        {
            panelActions.SetActive(true);
        }
        else
        {
            panelActions.SetActive(false);
        }

        //desativar todos os outros painéis irrelevantes

        cursorUI.Cursor.SetActive(false);
        panelConfirmAttack.SetActive(false);
    }

    private void TargetDisplayHandle()
    {
        
        cursorUI.Cursor.SetActive(true);
        panelConfirmAttack.SetActive(true);

        //procurar o que o jogador está pressionando
        
            if (Input.GetKeyDown(KeyCode.A))
            {
              pressedBtn = true;
            }
            if (Input.GetKeyUp(KeyCode.A) && pressedBtn)
            {
                    inimigoAlvo = cursorUI.SelecionarInimigo(KeyCode.A);
                    pressedBtn = false;
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
               pressedBtn = true;
            }
            if (Input.GetKeyUp(KeyCode.D) && pressedBtn)
            {
                inimigoAlvo = cursorUI.SelecionarInimigo(KeyCode.D);
                pressedBtn = false;
            }


        //Atualizar o alvo


        //Desativar painéis não utilizados
        panelActions.SetActive(false);
    }

    public void ConfirmaAtaque()
    {
        currentDisplay = ScreenDisplays.NEUTRALDISPLAY; //depois de confirmar um ataque, vamos direto ao calculo de dano e voltamos ao display neutro

        BattleHandler.playerUsedAction = standbyAction;
        BattleHandler.inimAlvo = inimigoAlvo;
        BattleHandler.currentState = BattleHandler.BattleStates.ADDSTATUSEFFECT;

    }

    public void showAttacks()
    {
        currentDisplay = ScreenDisplays.ATTACKSDISPLAY;
    } //troca o estado de display para ATTACKDISPLAY (Acionado por via do botão "Ataque" no painel neutro) 

    public void showFantasia()
    {
        currentDisplay = ScreenDisplays.FANTASYDISPLAY;

    }

    private void OnGUI()
    {

        if(BattleHandler.currentState == BattleHandler.BattleStates.PLAYERCHOICE && currentDisplay == ScreenDisplays.ATTACKSDISPLAY)
        {
            PlayerAttackDisplay();
        }


    }

    public void PlayerAttackDisplay() //cria os botõe em GUI de movimentos que o jogador pode usar
    {
        if (GUI.Button(new Rect(panelActions.transform.position.x + adjustX , panelActions.transform.position.y + adjustY, 75, 30), GameInformation.playerActionUm.ActionName))
        {
            //colocar os cálculos de dano e o movimento que está sendo usado
            standbyAction = GameInformation.playerActionUm;

            currentDisplay = ScreenDisplays.TARGETDISPLAY; 

            
        }

        if (GUI.Button(new Rect(panelActions.transform.position.x + adjustX, panelActions.transform.position.y + adjustY + 50, 75, 30), GameInformation.playerActionDois.ActionName))
        {
            //colocar os cálculos de dano aqui
            standbyAction = GameInformation.playerActionDois;

            currentDisplay = ScreenDisplays.TARGETDISPLAY;
        }

        if (GUI.Button(new Rect(panelActions.transform.position.x + adjustX, panelActions.transform.position.y + adjustY + 100, 75, 30), GameInformation.playerActionTres.ActionName))
        {
            //colocar os cálculos de dano aqui
            standbyAction = GameInformation.playerActionTres;

            currentDisplay = ScreenDisplays.TARGETDISPLAY;    
        }

        if (GUI.Button(new Rect(Screen.width -1000, Screen.height - 50, 45, 45),"Volta"))
        {
            //colocar os cálculos de dano aqui

            currentDisplay = ScreenDisplays.NEUTRALDISPLAY;
        }
    }

  

}
