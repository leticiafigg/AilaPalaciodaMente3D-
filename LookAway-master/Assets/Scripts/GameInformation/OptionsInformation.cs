using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsInformation : MonoBehaviour
{
    public static bool FullscreenSetting;
    public static float MasterVol;
    public GameObject MasterSlider;

    private void Start()
    {  
        MasterVol = 1;
        LoadInformation.LoadOptions();   
    }

    public void Masterchange()
    {
        MasterSlider = GameObject.FindGameObjectWithTag("MasterVolSlider");

        MasterVol = MasterSlider.GetComponent<Slider>().value;
        AudioListener.volume = MasterVol;
    }

    public void FullscreenToggle()
    {
        FullscreenSetting = !FullscreenSetting;
        Screen.fullScreen = FullscreenSetting;
    }

    public void SalvarOptions()
    {
        SaveInformation.SaveOptions();
    }

     

}
