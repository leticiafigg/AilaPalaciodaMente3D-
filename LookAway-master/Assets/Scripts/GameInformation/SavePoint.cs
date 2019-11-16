using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    public BaseHUDHandler hudHandleScript;
    public MoveChanPhisical playerPhisical;
    private string intrName;
    private bool pressedBtn;

    // Start is called before the first frame update
    void Start()
    {
        pressedBtn = false;
        intrName = "Salvar";
    }

    // Update is called once per frame
    void Update()
    {
        if (hudHandleScript.interactOn)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                pressedBtn = true;
            }

            if (Input.GetKeyUp(KeyCode.E) && pressedBtn)
            {
                GameInformation.LastScene = SceneManager.GetActiveScene().name;
                GameInformation.LastPos = playerPhisical.GetPlayerPos();

                SaveInformation.SaveAll();

                Debug.Log("Saved!");

                pressedBtn = false;
            }


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        //Ao ativar o trigger, (o jogador está perto) mudar o nome da interação a ser feita para Salvar
       if(other.gameObject.CompareTag("Player"))
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
