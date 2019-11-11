using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoHandle : MonoBehaviour
{
    public BaseHUDHandler hudHandleScript;
    public GameObject TextBoxObj;
    private string intrName;
    private bool pressedBtn;

    public List<string> Locutores;

    // Start is called before the first frame update
    void Start()
    {
        pressedBtn = false;
        intrName = "Falar";
    }

    // Update is called once per frame
    void Update()
    {
        if (hudHandleScript.interactOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressedBtn = true;
            }

            if (Input.GetKeyUp(KeyCode.E) && pressedBtn)
            {
                TextBoxObj.SetActive(true);
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hudHandleScript.AtivarPrompt(intrName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Quando o Player sair, a interação se torna vazia e o prompt deve ser desativado
        if (other.gameObject.CompareTag("Player"))
        {

            hudHandleScript.DesativarPrompt();
        }
    }
}
