using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreation : MonoBehaviour
{

    private BasePlayer newPlayer;
    public GameObject informationStore;
    private GameInformation informationAdd;

    private StatCalc statCalcScript = new StatCalc();

    private void Start()
    {
        newPlayer = new BasePlayer();

        newPlayer.PlayerName = "Aila";
        newPlayer.PlayerLevel = 1;
        newPlayer.Poder = 15;
        newPlayer.Imaginacao = 12;
        newPlayer.Determinacao = 10;
        newPlayer.Armadura = 0;
        newPlayer.Sorte = 5;
        newPlayer.XPAtual = 0;
        newPlayer.XPNecessario = 300;

        GameInformation.Aila = newPlayer;
        GameInformation.AilaPV = statCalcScript.CalcularPV(GameInformation.Aila.Determinacao);
        GameInformation.AilaPF = statCalcScript.CalcularPF(GameInformation.Aila.Imaginacao);
        GameInformation.AilaPVatual = GameInformation.AilaPV;
        GameInformation.AilaPFatual = GameInformation.AilaPF;
        GameInformation.Aila.AilaClass = newPlayer.AilaArch(BasePlayer.AilaArchetype.CORAJOSA);
        

        SaveInformation.SaveAll();
    }

    private void Update()
    {
        
    }


}
