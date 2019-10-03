using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    public string Nome;
    public int totalHp; //cada inimigo tem um nome para identificá-lo, uma quantidade de vida e de stun
    public int totalSP;
    public int Armadura;

    public int hpatual;
    private int spatual;

    public bool agiu;
    public bool derrotado;

    public GameObject inimigoobj;

    private void Start()
    {
        agiu = false;
        derrotado = false;
        hpatual = totalHp;
        spatual = totalSP;
    } 

    // Update is called once per frame
    void Update()
    {
      if(hpatual <= 0)
      {
            Destroy(inimigoobj);
            
           
      }
    }

    public void TakeDamage(int dmg)
    {
       hpatual = hpatual - (dmg - Armadura); //O dano é reduzido diretamente da Armadura, por enquanto
        derrotado = true;
    }
}
