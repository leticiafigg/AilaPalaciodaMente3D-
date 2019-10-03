using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreation : MonoBehaviour
{

    private BasePlayer newPlayer;
    public GameObject informationStore;
    private GameInformation informationAdd;

    private void Start()
    {
        newPlayer = new BasePlayer();

        newPlayer.PlayerName = "Aila";
        newPlayer.PlayerLevel = 1;
        newPlayer.Poder = 10;
        newPlayer.Imaginacao = 10;
        newPlayer.Determinacao = 10;
        newPlayer.Armadura = 0;
        newPlayer.Sorte = 5;
        newPlayer.XPAtual = 0;
        newPlayer.XPNecessario = 300;

        GameInformation.Aila = newPlayer;
        SaveInformation.SaveAll();
    }

    private void Update()
    {
        
    }


}
