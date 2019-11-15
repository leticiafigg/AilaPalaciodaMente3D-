using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsInformation : MonoBehaviour
{
    private void Awake()
    {
        LoadInformation.LoadOptions(); 
    }

    private void FixedUpdate()
    {
        
    }

    public static float MasterVol;
    public static float MasterDesejado;
    public static Slider Masterslider;

    public void Masterchange()
    {
        MasterDesejado = Masterslider.value;
        AudioListener.volume = MasterDesejado;
    }

    public void SalvarOptions()
    {
        MasterVol = MasterDesejado;
        AudioListener.volume = MasterVol;

        SaveInformation.SaveOptions();
    }

    public void DiscardChanges()
    {
        AudioListener.volume = MasterVol;
    }
    





}
