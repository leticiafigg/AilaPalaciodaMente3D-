using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool HealthPU;
    public bool JumpPU;
    
    public GameObject player;
    public int jumpspeedTemp; //A força adicional temporária adicionada ao jogador
    public bool coletado;

    

    // Start is called before the first frame update
    void Awake()
    {
        coletado = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerEnhance(); //método a ser usado para mudar valores e buffar/ debuffar o player (Especialmente em movimentação livre)
            coletado = true;
        }
    }

    private void PlayerEnhance() //Aprimora status diversos do player dependendo de qual booleana é verdadeira
    {
        if(JumpPU)
        {
            player.GetComponent<MoveChanPhisical>().SuperJumpEnabled(jumpspeedTemp);
        }

        if(HealthPU)
        {
            GameInformation.AilaPVatual += (int)(GameInformation.AilaPV * 0.15f);

            if(GameInformation.AilaPVatual > GameInformation.AilaPV)
            {
                GameInformation.AilaPVatual = GameInformation.AilaPV;
            }
        }

    }

}
