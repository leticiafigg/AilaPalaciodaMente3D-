using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenObj;
    public GameObject playerObj;
    public GameObject baseHudObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameInformation.AilaPVatual <= 0)
        {
            deathScreenObj.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            playerObj.SetActive(false); //desativa o jogador para que ele não possa fazer mais nada 
            baseHudObj.SetActive(false);
        }
    }

    public void CarregarSave()
    {
        LoadInformation.LoadAll();
        if (GameInformation.LastScene != null && GameInformation.LastScene != "")
        {
            SceneManager.LoadScene(GameInformation.LastScene);
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
