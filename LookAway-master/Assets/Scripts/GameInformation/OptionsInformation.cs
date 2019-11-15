using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsInformation : MonoBehaviour
{
    public static Slider Masterslider;
    public static bool FullscreenSetting;
    public static float MasterVol;
    

    private void Awake()
    {
        Masterslider = GameObject.FindGameObjectWithTag("MasterVolSlider").GetComponent<Slider>();
        MasterVol = 1;
        LoadInformation.LoadOptions();
        Masterslider.value = MasterVol;
    }

    public void Masterchange()
    {
        MasterVol = Masterslider.value;
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
