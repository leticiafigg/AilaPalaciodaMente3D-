using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public static Vector3 LastPos;
    public static string  LastScene;


    public static BasePlayer Aila { get; set;}



    private void Update()
    {
        
    }

}
