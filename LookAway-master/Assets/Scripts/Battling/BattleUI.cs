using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleUI : MonoBehaviour
{
    private BaseAction standbyAction;
    private bool pressedBtn;
    private KeyCode pressedKey;

    public static Inimigo inimigoAlvo;

    public GameObject panelActions;
    public GameObject panelAttacks;
    public GameObject panelConfirmAttack;
    public GameObject cancelPanelObj;
    public GameObject descriptionTxtObj;
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
        if (BattleHandler.currentActor == BattleHandler.BattleStates.PLAYERCHOICE) //caso o display seja do menu neutro, *E* estamos no estado "escolha do jogador" ativa o painel neutro
        {
            panelActions.SetActive(true);
        }
        else
        {
            panelActions.SetActive(false);
        }

        //desativar todos os outros painéis irrelevantes
        cancelPanelObj.SetActive(false);
        cursorUI.Cursor.SetActive(false);
        panelConfirmAttack.SetActive(false);
        panelAttacks.SetActive(false);
    }

    private void TargetDisplayHandle()
    {
        //Ativar painéis relevantes e desativar os irrelevantes
        cursorUI.Cursor.SetActive(true);
        panelConfirmAttack.SetActive(true);
        cancelPanelObj.SetActive(true);
        panelActions.SetActive(false);
        panelAttacks.SetActive(false);

        //procurar o que o jogador está pressionando

        if (Input.GetKeyDown(KeyCode.A))
            {
              pressedBtn = true;
            }
            if (Input.GetKeyUp(KeyCode.A) && pressedBtn)
            {
                    cursorUI.SelecionarInimigo(KeyCode.A);
                    pressedBtn = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                    pressedBtn = true;
            }
            if (Input.GetKeyUp(KeyCode.D) && pressedBtn)
            {
                    cursorUI.SelecionarInimigo(KeyCode.D);
                    pressedBtn = false;
            }  
    }

    public void ConfirmaAtaque()
    {
        currentDisplay = ScreenDisplays.NEUTRALDISPLAY; //depois de confirmar um ataque, vamos direto ao calculo de dano e voltamos ao display neutro

        inimigoAlvo = cursorUI.RetornarAlvo();          //adquire o status do inimigo destacado no momento da confirmação 

        BattleHandler.playerUsedAction = standbyAction; //ação selecionada durante o showattacks

        BattleHandler.inimAlvo = inimigoAlvo;
        BattleHandler.currentState = BattleHandler.BattleStates.ADDSTATUSEFFECT;

    }

    public void showAttacks()
    {
        currentDisplay = ScreenDisplays.ATTACKSDISPLAY; //troca o estado de display para ATTACKDISPLAY (Acionado por via do botão "Ataque" no painel neutro) 
        cancelPanelObj.SetActive(true);
        panelAttacks.SetActive(true);
        panelActions.SetActive(false);
    } 

    public void showFantasia()
    {
        currentDisplay = ScreenDisplays.FANTASYDISPLAY;
        cancelPanelObj.SetActive(true);
        panelActions.SetActive(false);
    }

    public void showNeutral()
    {
        currentDisplay = ScreenDisplays.NEUTRALDISPLAY;
        cancelPanelObj.SetActive(false);
        panelActions.SetActive(true);
    }


    public void PlayerAttackChoice(string attackAction) //cria os botões em GUI de movimentos que o jogador pode usar
    {
        foreach (BaseAction learnedActions in GameInformation.AcoesAprendidas)
        {
            if (learnedActions.ActionName == attackAction)
            {
                standbyAction = learnedActions;

                if(GameInformation.AilaPFatual - standbyAction.ActionCost >= 0) //Se o custo da ação não deixaria Aila com PF negativos, então ela executa normalmente
                {
                    GameInformation.AilaPFatual -= standbyAction.ActionCost;
                    descriptionTxtObj.GetComponent<TextMeshProUGUI>().text = standbyAction.ActionDesc;
                    currentDisplay = ScreenDisplays.TARGETDISPLAY;
                }
                else
                {
                    BattleHandler.turnLogText = "PF insuficiente!";

                }
            }
        }

    }

  

}
