using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEvent : MonoBehaviour
{
    public DialogoHandle dialogoEvent;

    // Start is called before the first frame update
    void Start()
    {
        dialogoEvent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            dialogoEvent.enabled = true;
            dialogoEvent.DialogoTrigger();

        }

    }


}
