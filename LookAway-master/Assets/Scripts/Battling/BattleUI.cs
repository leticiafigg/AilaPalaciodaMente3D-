using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    private string playerName;
    private int playerLvl;
    private int playerPV;
    private int playerPE; //Ernergia, recurso usado para algumas ações

    // Start is called before the first frame update
    void Start()
    {
        playerName = GameInformation.Aila.PlayerName;
        playerLvl = GameInformation.Aila.PlayerLevel;
        //playerPV 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
