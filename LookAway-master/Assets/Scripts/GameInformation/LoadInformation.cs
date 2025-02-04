﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInformation : MonoBehaviour
{
    private static BasePlayer playerLoad;

     public static void LoadAll() //Apenas carrega os parâmetros que copõem um player do disco, se player name não for nulo.
     {
        playerLoad = new BasePlayer();

        if (PlayerPrefs.HasKey("PLAYERLEVEL"))
        {
            GameInformation.Aila = playerLoad;

            GameInformation.Aila.PlayerLevel = PlayerPrefs.GetInt("PLAYERLEVEL");
            GameInformation.Aila.PlayerName = PlayerPrefs.GetString("PLAYERNAME");
            GameInformation.Aila.Imaginacao = PlayerPrefs.GetInt("IMAGINACAO");
            GameInformation.Aila.Determinacao = PlayerPrefs.GetInt("DETERMINACAO");
            GameInformation.Aila.Armadura = PlayerPrefs.GetInt("ARMADURA");
            GameInformation.Aila.Sorte = PlayerPrefs.GetInt("SORTE");
            GameInformation.AilaPV = PlayerPrefs.GetInt("PVTOTAL");
            GameInformation.AilaPVatual = PlayerPrefs.GetInt("PVATUAL");
            GameInformation.AilaPF = PlayerPrefs.GetInt("PFTOTAL");
            GameInformation.AilaPFatual = PlayerPrefs.GetInt("PFATUAL");
            GameInformation.LastScene = PlayerPrefs.GetString("LASTSCENE");

        }
    }
}
