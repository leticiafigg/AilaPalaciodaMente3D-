using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraImpedimento : MonoBehaviour
{
    public EnterEvent coxinhaBossDefeated;
    public DialogoHandle coxinhaBossDefeatDialogue;
    public BoxCollider eventCollider;
    public GameObject Barreiravestigio;
    public GameObject CoxinhaBoss;
    // Start is called before the first frame update
    void Start()
    {
        coxinhaBossDefeated.enabled = false;
        coxinhaBossDefeatDialogue.enabled = false;
        eventCollider.enabled = false;

        if (GameInformation.coxinhabossWon)
        {
            
            eventCollider.enabled = true;
            coxinhaBossDefeated.enabled = true;
            coxinhaBossDefeatDialogue.enabled = true;
            Barreiravestigio.SetActive(false);
            CoxinhaBoss.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
