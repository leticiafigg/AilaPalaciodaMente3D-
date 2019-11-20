using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraImpedimento : MonoBehaviour
{
    public EnterEvent coxinhaBossDefeated;
    public GameObject Barreiravestigio;

    // Start is called before the first frame update
    void Awake()
    {
        coxinhaBossDefeated.enabled = false;

        if(GameInformation.coxinhabossWon)
        {
            coxinhaBossDefeated.enabled = true;
            Barreiravestigio.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
