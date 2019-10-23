using System.Collections;
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
        PlayerPrefs.SetInt("PFTOTAL", GameInformation.AilaPF);

        Debug.Log("Saved All Information!");
    }

}
