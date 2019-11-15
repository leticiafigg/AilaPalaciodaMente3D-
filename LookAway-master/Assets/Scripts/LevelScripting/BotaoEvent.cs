using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoEvent : MonoBehaviour
{
    public BaseHUDHandler hudScript;
    public string actionName;
    public GameObject portaForno;
    public GameObject portaDestino;
    public GameObject botaoDestino;
    public DialogoHandle dialogoBotao;
    public AudioSource pressionarSFX;
    private bool pressedBtn;
    private bool jaApertou;



    // Start is called before the first frame update
    void Start()
    {
        pressedBtn = false;
        jaApertou = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hudScript.interactOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressedBtn = true;
            }
            if (Input.GetKeyUp(KeyCode.E) && pressedBtn && !jaApertou)
            {
               
                pressionarSFX.Play();
                pressedBtn = false;
                jaApertou = true;

                dialogoBotao.DialogoTrigger();
            }
        }

        if (jaApertou)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, botaoDestino.transform.position, 5 * Time.deltaTime);
            portaForno.transform.position = Vector3.MoveTowards(portaForno.transform.position, portaDestino.transform.position, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hudScript.AtivarPrompt(actionName);
        }

    }
}
