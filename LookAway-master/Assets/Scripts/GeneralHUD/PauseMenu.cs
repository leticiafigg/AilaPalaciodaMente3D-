using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CloseOptionsInGame()
    {
        SaveInformation.SaveOptions();
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Player").GetComponent<MoveChanPhisical>().enabled = true;
    }

}
