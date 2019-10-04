using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battlerender : MonoBehaviour
{
    //public int index;
    public string EnemyType; //Vamos Definir o tipo de combate pela Cena 
    string ActualScene;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefsX.SetVector3("OldPlayerPosition", other.transform.position - other.transform.forward * 2);
            GameInformation.LastPos = other.transform.position;
            GameInformation.LastScene = "mapa1";
            SceneManager.LoadScene(EnemyType);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
