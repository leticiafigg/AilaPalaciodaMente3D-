using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraImpedimento : MonoBehaviour
{
    public EnterEvent coxinhaBossDefeated;
    public DialogoHandle coxinhaBossDefeatDialogue;
    public GameObject Barreiravestigio;

    // Start is called before the first frame update
    void Awake()
    {
        coxinhaBossDefeated.enabled = false;
        coxinhaBossDefeatDialogue.enabled = false;

        if (GameInformation.coxinhabossWon)
        {
            coxinhaBossDefeated.enabled = true;
            coxinhaBossDefeatDialogue.enabled = true;
            Barreiravestigio.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
