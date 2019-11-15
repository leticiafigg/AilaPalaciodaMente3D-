using System.Collections;
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
            GameInformation.FragmentosDeMemoria = PlayerPrefs.GetInt("FRAGMENTOSDEMEMORIA");
            GameInformation.LastScene = PlayerPrefs.GetString("LASTSCENE");
            GameInformation.LastPos = PlayerPrefsX.GetVector3("SavePlayerPos");


            List<BaseAction> AcoesSave = new List<BaseAction>(); //Estamos carregando uma lista específica manualmente por enquanto
            AcoesSave.Add(new AttackAction());
            AcoesSave.Add(new Shove());
            AcoesSave.Add(new Pat());
            AcoesSave.Add(new ToqueChocante());

            GameInformation.AcoesAprendidas = AcoesSave;
            
        }
     }

     public static void LoadOptions()
     {
        if (PlayerPrefs.HasKey("PLAYEROPTIONS"))
        {

            OptionsInformation.MasterVol = PlayerPrefs.GetFloat("MASTERVOLUME");


        }

     }
}
