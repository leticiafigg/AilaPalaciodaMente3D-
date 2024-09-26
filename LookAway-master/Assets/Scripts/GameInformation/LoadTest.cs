using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NovoJogo()
    {
       
        SceneManager.LoadScene("AilaIsBorn");

    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("LASTSCENE"))
        {
            LoadInformation.LoadAll();

            if (GameInformation.LastScene != null)
            {
                GameInformation.loadingSave = true;
                SceneManager.LoadScene(GameInformation.LastScene);
            }
        }
        else
        {

        }
    }
}
