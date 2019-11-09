using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    private string Nome;
    

    public int totalHp; //cada inimigo tem um nome para identificá-lo, uma quantidade de vida e de stun
    public int totalSP;
    public int hpatual;
    private int spatual;

    private int enemylvl;
    public int maxlvl;     //Status gerais (Exceto a Armadura) mudarão de acordo com o nível do inimigo,
    public int poder;      // - mas serão baseados numa predefinição dada no prefab, para facilitar implementação
    public int imaginacao;
    public int resistencia;
    public int determinacao;
    public int armadura;   // A armadura é específica para cada tipo de inimigo
    public int sorte;
    
    public bool agiu;
    public bool atordoado;
    public bool derrotado;

    public enum EnemyState
    {
        BEM,
        AGRESSIVO,
        MORRENDO
    }

    private EnemyState estadoAtual;

    public GameObject inimigoobj;
     

    public int EnemyLevel
    {
        get { return enemylvl; }
        set { enemylvl = value; }
    }

    public EnemyState EstadoAtual
    {
        get { return estadoAtual; }
        set { estadoAtual = value; }
    }

    private void Start()
    {
        Nome = inimigoobj.name;
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


      if(this.hpatual <= this.totalHp/2 || GameInformation.AilaPVatual <= GameInformation.AilaPV/2)
      {
            this.EstadoAtual = EnemyState.AGRESSIVO;

            if (this.hpatual <= this.totalHp / 4)
            {
                this.EstadoAtual = EnemyState.MORRENDO;
            }
      }
      else
      {
            this.EstadoAtual = EnemyState.BEM;
      }
      

    }

    public void TakeDamage(int dmg)
    {
       hpatual = hpatual - (int)(dmg - ((determinacao * 0.5 ) + armadura) ); //O dano é reduzido por metade da determinação , mais a armadura
        derrotado = true;
    }
}
