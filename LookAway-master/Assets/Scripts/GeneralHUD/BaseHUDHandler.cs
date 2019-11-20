using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BaseHUDHandler : MonoBehaviour
{
    public float hudSpeed;
    public float savePopUpStartTime;
    private float savetime;
    private bool statMenuligado;
    private static bool savePopUpligado;
    private bool pressedBtn;

    public GameObject sliderPvObj;
    public GameObject pvText;
    private string pvMinMaxstrg;
    private float pvMax;
    private float pvMin;

    public GameObject sliderPfObj;
    public GameObject pfText;
    private string pfMinMaxstrg;
    private float pfMax;
    private float pfMin;

    public GameObject interactPrompt;
    public GameObject interactionNameText;
    public string interactionName;
    public bool interactOn;
    public bool interactSave;

    public GameObject savePopUpObj;
    public GameObject saveTraslateTransform;
    public GameObject saveOriginalPoint;

    public GameObject tabMenuPanel;
    public GameObject tabTraslatePoint;
    public GameObject tabOriginalPoint;
    public TextMeshProUGUI playerLvlTxt;
    public TextMeshProUGUI poderStatTxt;
    public TextMeshProUGUI imaginacaoStatTxt;
    public TextMeshProUGUI resitenciaStatTxt;
    public TextMeshProUGUI determinacaoStatTxt;
    public TextMeshProUGUI sorteStatTxt;

    public TextMeshProUGUI xpAtualTxt;
    public TextMeshProUGUI xpParaoProximo;
    // Start is called before the first frame update
    void Start()
    {
        interactOn = false;
        interactSave = false;
        savePopUpligado = false;
        pvMax = GameInformation.AilaPV;
        pvMin = 0;

        pfMax = GameInformation.AilaPF;
        pfMin = 0;

        SetSlider();

        AtualizarTabBox();
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        //atualiza os valores máximos e atuais
        pvMinMaxstrg = "PV: " + GameInformation.AilaPVatual + "/" + GameInformation.AilaPV;
        pvText.GetComponent<TextMeshProUGUI>().text = pvMinMaxstrg;

        pfMinMaxstrg = "PF: " + GameInformation.AilaPFatual + "/" + GameInformation.AilaPF;
        pfText.GetComponent<TextMeshProUGUI>().text = pfMinMaxstrg;

        //atualiza a posição da barra do slider de acordo com a vida total (Calculado automáticamente pelo Slider)
        sliderPvObj.GetComponent<Slider>().value = GameInformation.AilaPVatual;
        sliderPfObj.GetComponent<Slider>().value = GameInformation.AilaPFatual;

        if(interactOn)
        {
            interactPrompt.SetActive(true);
        }
        else
        {
            interactPrompt.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pressedBtn = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab) && pressedBtn)
        { 
            ToggleUpMenu();
            pressedBtn = false;
        }
       
        if(statMenuligado)
        {
            tabMenuPanel.transform.position = Vector3.MoveTowards(tabMenuPanel.transform.position,tabTraslatePoint.transform.position , hudSpeed * Time.deltaTime);
        }
        else
        {
            tabMenuPanel.transform.position = Vector3.MoveTowards(tabMenuPanel.transform.position, tabOriginalPoint.transform.position, hudSpeed * Time.deltaTime);
        }

        if (savePopUpligado)
        {
            savePopUpObj.transform.position = Vector3.MoveTowards(savePopUpObj.transform.position, saveTraslateTransform.transform.position, hudSpeed * Time.deltaTime);

            savetime -= Time.deltaTime;
            if (savetime <= 0)
            {
              ToggleSavePopUp();
              savetime = savePopUpStartTime;
            }
        }
        else
        {
            savePopUpObj.transform.position = Vector3.MoveTowards(savePopUpObj.transform.position, saveOriginalPoint.transform.position, hudSpeed * Time.deltaTime);
        }

       
        
    }

    public static void ToggleSavePopUp()
    {
        savePopUpligado = !savePopUpligado;
    }

    private void ToggleUpMenu() //alternar entre ligar e desligar o menu de status
    {
        statMenuligado = !statMenuligado;
        AtualizarTabBox();       
    }

    private void AtualizarTabBox() //Sempre que for chamado atualiza o que deve estar escrito
    {
        playerLvlTxt.text = "Aila Lv." + GameInformation.Aila.PlayerLevel;
        poderStatTxt.text = "Poder: " + GameInformation.Aila.Poder;
        imaginacaoStatTxt.text = "Imaginação: " + GameInformation.Aila.Imaginacao;
        resitenciaStatTxt.text = "Resistência: " + GameInformation.Aila.Resistencia;
        determinacaoStatTxt.text = "Determinação: " + GameInformation.Aila.Determinacao;
        sorteStatTxt.text = "Sorte: " + GameInformation.Aila.Sorte;

        xpAtualTxt.text = "Exp. atual: " + GameInformation.Aila.XPAtual;
        xpParaoProximo.text = "Próximo nível: " + GameInformation.Aila.XPNecessario;
    }

    private void SetSlider()
    {

        //set de Pontos de Vida
        sliderPvObj.GetComponent<Slider>().minValue = pvMin;
        sliderPvObj.GetComponent<Slider>().maxValue = pvMax;

        sliderPvObj.GetComponent<Slider>().value = GameInformation.AilaPVatual;

        //set de Pontos de Fantasia
        sliderPfObj.GetComponent<Slider>().minValue = pfMin;
        sliderPfObj.GetComponent<Slider>().maxValue = pfMax;

        sliderPfObj.GetComponent<Slider>().value = GameInformation.AilaPFatual;
    }

    public void AtivarPrompt(String interactionName)
    {
       interactOn = true;
       
       interactionNameText.GetComponent<TextMeshProUGUI>().text = interactionName;
    }

    public void DesativarPrompt()
    {
       interactOn = false;

       interactionNameText.GetComponent<TextMeshProUGUI>().text = " ";
    }

    public void AtivarSavePrompt(String interactionName)
    {
        interactSave = true;

        interactionNameText.GetComponent<TextMeshProUGUI>().text = interactionName;
    }

    public void DesativarSavePrompt()
    {
        interactSave = false;

        interactionNameText.GetComponent<TextMeshProUGUI>().text = " ";
    }

}
