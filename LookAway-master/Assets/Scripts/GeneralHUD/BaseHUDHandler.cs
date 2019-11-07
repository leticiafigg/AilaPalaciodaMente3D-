using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseHUDHandler : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        interactOn = false;

        pvMax = GameInformation.AilaPV;
        pvMin = 0;

        pfMax = GameInformation.AilaPF;
        pfMin = 0;

        SetSlider();
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //atualiza os valores máximos e atuais
        pvMinMaxstrg = "PV: " + GameInformation.AilaPVatual + "/" + GameInformation.AilaPV;
        pvText.GetComponent<Text>().text = pvMinMaxstrg;

        pfMinMaxstrg = "PV: " + GameInformation.AilaPFatual + "/" + GameInformation.AilaPF;
        pfText.GetComponent<Text>().text = pfMinMaxstrg;

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
 
    }

    private void SetSlider()
    {

        //set de Pontos de Vida
        sliderPvObj.GetComponent<Slider>().minValue = pvMin;
        sliderPvObj.GetComponent<Slider>().maxValue = pvMax;

        sliderPvObj.GetComponent<Slider>().value = GameInformation.AilaPVatual % pvMax;

        //set de Pontos de Fantasia
        sliderPfObj.GetComponent<Slider>().minValue = pfMin;
        sliderPfObj.GetComponent<Slider>().maxValue = pfMax;

        sliderPfObj.GetComponent<Slider>().value = GameInformation.AilaPFatual % pfMax;
    }

    public void AtivarPrompt(String interactionName)
    {
       interactOn = true;
       
       interactionNameText.GetComponent<Text>().text = interactionName;
    }

    public void DesativarPrompt()
    {
       interactOn = false;

       interactionNameText.GetComponent<Text>().text = " ";
    }
}
