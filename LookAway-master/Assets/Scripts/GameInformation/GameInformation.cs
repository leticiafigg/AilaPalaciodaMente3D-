﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    //public static List<BaseAction> playerActions;
   

    public static Vector3 LastPos;
    public static string  LastScene;

    public static BasePlayer Aila { get; set;}
    

    public static int AilaPV { get; set; }
    public static int AilaPF { get; set; }
    public static int AilaPVatual { get; set; }
    public static int AilaPFatual { get; set; }

    public static BaseAction playerActionUm = new AttackAction(); // podem ser várias, e podemos adicionar outras dependendo do arquétipo/classe da Aila
    public static BaseAction playerActionDois = new Shove();
    public static BaseAction playerActionTres = new Pat();

    private void Update()
    {
        
    }

}
