using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool HealthPU;
    public bool JumpPU;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            PlayerEnhance(); //método a ser usado para mudar valores e buffar/ debuffar o player (Especialmente em movimentação livre)

           Destroy(this.gameObject);
            

        }


    }

    private void PlayerEnhance()
    {
        if (JumpPU)
        {
            player.GetComponent<MoveChanPhisical>().jumpspeed = 20000;
        }
    }
}
