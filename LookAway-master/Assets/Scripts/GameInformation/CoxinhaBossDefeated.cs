using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoxinhaBossDefeated : MonoBehaviour
{
    
    private bool battleWon;
    // Start is called before the first frame update
    void Start()
    {
        battleWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(BattleHandler.currentState == BattleHandler.BattleStates.WIN)
        {
            GameInformation.coxinhabossWon = true;
        }
    }
}
