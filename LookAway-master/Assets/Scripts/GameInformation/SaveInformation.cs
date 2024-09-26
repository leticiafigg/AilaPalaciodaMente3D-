﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInformation 
{
   
    
    public static void SaveAll()
    {

        PlayerPrefs.SetInt("PLAYERLEVEL", GameInformation.Aila.PlayerLevel);
        PlayerPrefs.SetString("PLAYERNAME", GameInformation.Aila.PlayerName);
        PlayerPrefs.SetInt("IMAGINACAO", GameInformation.Aila.Imaginacao);
        PlayerPrefs.SetInt("DETERMINACAO", GameInformation.Aila.Determinacao);
        PlayerPrefs.SetInt("ARMADURA", GameInformation.Aila.Armadura);
        PlayerPrefs.SetInt("SORTE", GameInformation.Aila.Sorte);
        PlayerPrefs.SetInt("PVTOTAL", GameInformation.AilaPV);
        PlayerPrefs.SetInt("PVATUAL", GameInformation.AilaPVatual);
        PlayerPrefs.SetInt("PFTOTAL", GameInformation.AilaPF);
        PlayerPrefs.SetInt("PFATUAL", GameInformation.AilaPFatual);
        PlayerPrefs.SetInt("FRAGMENTOSDEMEMORIA", GameInformation.FragmentosDeMemoria);
        PlayerPrefs.SetString("LASTSCENE", GameInformation.LastScene);
        PlayerPrefsX.SetVector3("SavePlayerPos", GameInformation.LastPos);

        PlayerPrefsX.SetBool("COXINHABOSSDEFEATED", GameInformation.coxinhabossWon);

        Debug.Log("Saved All Information!");
    }

    public static void SaveOptions()
    {
        PlayerPrefs.SetFloat("MASTERVOLUME", OptionsInformation.MasterVol);
    }

}
