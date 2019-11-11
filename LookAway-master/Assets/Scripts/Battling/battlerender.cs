using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battlerender : MonoBehaviour
{
    //public int index;
    public string EnemyType; //Vamos Definir o tipo de combate pela Cena 
    string ActualScene;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameInformation.LastScene = SceneManager.GetActiveScene().name;
            PlayerPrefsX.SetVector3("OldPlayerPosition", collision.transform.position - collision.transform.forward * 2);

            GameInformation.LastPos = PlayerPrefsX.GetVector3("OldPlayerPosition");

            SceneManager.LoadScene(EnemyType);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
}
