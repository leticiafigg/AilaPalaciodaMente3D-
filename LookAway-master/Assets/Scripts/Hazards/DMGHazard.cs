using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGHazard : MonoBehaviour
{
    public GameObject perigoObj;
    public int danocausado;

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
        //ao detectar a colisão o objeto checa se é o jogador, se for ele causa dano diretamente na vida

        if(collision.gameObject.tag == "Player")
        {

            GameInformation.AilaPVatual -= danocausado;

        }

    }
}
