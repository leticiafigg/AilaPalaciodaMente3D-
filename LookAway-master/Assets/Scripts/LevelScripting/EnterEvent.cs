using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEvent : MonoBehaviour
{
    public DialogoHandle dialogoEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            dialogoEvent.DialogoTrigger();

        }

    }


}
