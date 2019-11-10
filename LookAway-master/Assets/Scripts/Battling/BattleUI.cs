using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleUI : MonoBehaviour
{
    private string playerName;
    private int playerLvl;
    private int playerPV;
    private int playerPE; //Ernergia, recurso usado para algumas ações
    
    public GameObject panelActions;
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
        playerName = GameInformation.Aila.PlayerName;
        playerLvl = GameInformation.Aila.PlayerLevel;
        currentDisplay = ScreenDisplays.NEUTRALDISPLAY;
        //playerPV 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MainPanelHandle();       
    }

    private void MainPanelHandle()
    {
        if (BattleHandler.currentState != BattleHandler.BattleStates.PLAYERCHOICE || currentDisplay != ScreenDisplays.NEUTRALDISPLAY) //usar isto no futuro para desabilitar todos os painéis juntos e mostrar apenas o que acontece na tela, para então... 
        {
            panelActions.SetActive(false);
        }

        if (BattleHandler.currentState == BattleHandler.BattleStates.PLAYERCHOICE && currentDisplay == ScreenDisplays.NEUTRALDISPLAY) // ... tornar aqueles inicialmente relevantes ativos quando for novamente o turno do jogador.
        {
            panelActions.SetActive(true);
        }
    }

    public void showAttacks()
    {
        currentDisplay = ScreenDisplays.ATTACKSDISPLAY;
    }

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
            BattleHandler.playerUsedAction = GameInformation.playerActionUm;

            currentDisplay = ScreenDisplays.NEUTRALDISPLAY; 

            BattleHandler.currentState = BattleHandler.BattleStates.ADDSTATUSEFFECT;
        }

        if (GUI.Button(new Rect(panelActions.transform.position.x + adjustX, panelActions.transform.position.y + adjustY + 50, 75, 30), GameInformation.playerActionDois.ActionName))
        {
            //colocar os cálculos de dano aqui
            BattleHandler.playerUsedAction = GameInformation.playerActionDois;

            currentDisplay = ScreenDisplays.NEUTRALDISPLAY;

            BattleHandler.currentState = BattleHandler.BattleStates.ADDSTATUSEFFECT;
        }

        if (GUI.Button(new Rect(panelActions.transform.position.x + adjustX, panelActions.transform.position.y + adjustY + 100, 75, 30), GameInformation.playerActionTres.ActionName))
        {
            //colocar os cálculos de dano aqui
            BattleHandler.playerUsedAction = GameInformation.playerActionTres;

            currentDisplay = ScreenDisplays.NEUTRALDISPLAY;

            BattleHandler.currentState = BattleHandler.BattleStates.ADDSTATUSEFFECT;
        }

        if (GUI.Button(new Rect(Screen.width -1000, Screen.height - 50, 45, 45),"Volta"))
        {
            //colocar os cálculos de dano aqui

            currentDisplay = ScreenDisplays.NEUTRALDISPLAY;
        }
    }
}
