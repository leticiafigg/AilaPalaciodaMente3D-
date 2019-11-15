using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoHandle : MonoBehaviour
{
    public BaseHUDHandler hudHandleScript;
    public GameObject TextBoxObj;
    public TextMeshProUGUI TextBoxText;

    public TextMeshProUGUI textBoxLocutorAtual;

    public Texture locutor1FileImg;
    public Texture locutor2FileImg;
    private GameObject locutor1GameObj; 
    private GameObject locutor2GameObj; 
    public string Locutor1Name;
    public string Locutor2Name;

    public List<string> Locutor0; //strings a serem adicionadas à lista
    public List<string> Locutor1;
    public List<string> Locutor2;
    public List<string> Locutor3;

    public List<List<string>> ConjuntoFalas; //Uma lista de lista de strings - A primeira é a lista dos "locutores" a segunda, suas respectivas falas, em ordem de interação

    private string falatual;
    public string intrName;
    public bool oneTimeEvent;
    private bool pressedBtn;
    private bool dialogoOpen;  

     
    private int locutor;
    private int falaIndex;

    // Start is called before the first frame update
    void Start()
    {
        locutor1GameObj = GameObject.FindGameObjectWithTag("Locutor1ImgObj");
        locutor2GameObj = GameObject.FindGameObjectWithTag("Locutor2ImgObj");
        locutor1GameObj.GetComponent<RawImage>().texture = locutor1FileImg;
        locutor2GameObj.GetComponent<RawImage>().texture = locutor2FileImg;

        textBoxLocutorAtual.text = Locutor1Name;
        locutor =0;
        falaIndex=0;
       
        pressedBtn = false;
        
        TextBoxObj.SetActive(false);
        dialogoOpen = false;

        ConjuntoFalas = new List<List<string>>();
        ConjuntoFalas.Add(Locutor0); //adicionando 4 locutores manualmente, pois as strings podem ser decididas via editor, e nem sempre serão necessários os 4 locutores
        ConjuntoFalas.Add(Locutor1);
        ConjuntoFalas.Add(Locutor2);
        ConjuntoFalas.Add(Locutor3);

        falatual = ConjuntoFalas[locutor][falaIndex]; //ao começar o script já setamos a fala atual como a primeira da primeira lista ([0][0])
    }

    // Update is called once per frame
    void Update()
    {
       

        TextBoxText.text = falatual;

        if(hudHandleScript.interactOn || dialogoOpen)
        {
            hudHandleScript.interactionName = intrName;

            if (Input.GetKeyDown(KeyCode.E))
            {
                pressedBtn = true;
            }

            if (Input.GetKeyUp(KeyCode.E) && pressedBtn)
            {
                if(dialogoOpen == false)
                {
                    OpenDialogo();
                }
                else
                {
                    AvancarTexto();
                }


                pressedBtn = false;
            }
        }


    }

    private void AvancarTexto()
    {
        if (falaIndex + 1 < ConjuntoFalas[locutor].Count) //se o indexador for menor que o total, podemos aumentá-lo e utilizá-lo normalmente
        {
            falaIndex++;
            falatual = ConjuntoFalas[locutor][falaIndex];
        }
        else if(locutor + 1 < ConjuntoFalas.Count) //se o indexador da fala era maior ou igual, mas ainda tem um locutor, passamos para o próximo locutor
        {
            locutor++;
            falaIndex = 0;
            falatual = ConjuntoFalas[locutor][falaIndex];
        }
        else
        {
            FimdeTexto();
        }

        if(locutor%2 == 0)              //Se a posição na lista for par, quem está falando é o primeiro locutor, se não, o segundo 
        {
           textBoxLocutorAtual.text = Locutor1Name;
        }
        else
        textBoxLocutorAtual.text = Locutor2Name;

       
    }

    private void FimdeTexto()
    {
        TextBoxObj.SetActive(false);
        dialogoOpen = false;

        if(oneTimeEvent)
        {
            this.enabled = false;
        }
    }

    private void OpenDialogo()       //liga a caixa de texto, desliga o prompt, mas avisa que o dialogo esta ligado
    {
        TextBoxObj.SetActive(true);
        hudHandleScript.DesativarPrompt();
        dialogoOpen = true;
    }

    public void DialogoTrigger()  //será utilizado para forçar a iniciação de uma conversa, sem input do jogador.
    {
       OpenDialogo();
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
