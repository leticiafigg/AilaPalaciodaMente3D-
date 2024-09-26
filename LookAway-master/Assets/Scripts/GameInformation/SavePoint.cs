using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    public BaseHUDHandler hudHandleScript;
    public AudioSource saveSoundFX;

    private MoveChanPhisical playerPhisical;
    private string intrName;
    private bool pressedBtn;

    // Start is called before the first frame update
    void Start()
    {
        playerPhisical = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveChanPhisical>();
        pressedBtn = false;
        intrName = "Salvar";
    }

    // Update is called once per frame
    void Update()
    {
        if (hudHandleScript.interactSave)
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
                BaseHUDHandler.ToggleSavePopUp();
                saveSoundFX.Play();

                pressedBtn = false;
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ao ativar o trigger, (o jogador está perto) mudar o nome da interação a ser feita para Salvar
       if(other.gameObject.CompareTag("Player"))
       {
            hudHandleScript.AtivarSavePrompt(intrName);      
       }      
    }

    private void OnTriggerExit(Collider other)
    {
        //Quando o Player sair, a interação se torna vazia e o prompt deve ser desativado
        if (other.gameObject.CompareTag("Player"))
        {
            hudHandleScript.DesativarSavePrompt();
        }
    }
}
