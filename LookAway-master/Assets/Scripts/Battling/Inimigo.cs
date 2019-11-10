using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    private string Nome;
    

    public int pvTotal; //cada inimigo tem um nome para identificá-lo, uma quantidade de vida e de stun
    public int stunTotal;
    public int pvAtual;
    public int stunAtual;

    private int enemylvl;
    public int maxlvl;     //Status gerais (Exceto a Armadura) mudarão de acordo com o nível do inimigo,
    public int poder;      // - mas serão baseados numa predefinição dada no prefab, para facilitar implementação
    public int imaginacao;
    public int resistencia;
    public int determinacao;
    public int armadura;   // A armadura é específica para cada tipo de inimigo
    public int sorte;

    public int PosInArray; //O próprio Inimigo salva a sua posição no array criado com BattleHandler>BattleStart

    private bool agiu;
    private bool atordoado;
    public bool derrotado;

    public GameObject inimigoobj;

    public enum EnemyState
    {
        BEM,
        AGRESSIVO,
        MORRENDO
    }

    private EnemyState estadoAtual;

    
     

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

    public bool Agiu
    {
        get { return agiu; }
        set { agiu = value; }
    }

    public bool Atordoado
    {
        get { return atordoado; }
        set { atordoado = value; }
    }

    private void Start()
    {
        Nome = inimigoobj.name;
        agiu = false;
        derrotado = false;
        pvAtual = pvTotal;
        stunAtual = stunTotal;
    } 

    // Update is called once per frame
    void Update()
    {
      if(pvAtual <= 0 && !derrotado)
      {
            BattleHandler.inimStatsList.RemoveAt(this.PosInArray); // quando morre é retirado da lista(por precaução) e também destrói o gameobject
            derrotado = true;
            Destroy(inimigoobj);  

      }


      if(this.pvAtual <= this.pvTotal/2 || GameInformation.AilaPVatual <= GameInformation.AilaPV/2)
      {
            this.EstadoAtual = EnemyState.AGRESSIVO;

            if (this.pvAtual <= this.pvTotal / 4)
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
       pvAtual = pvAtual - (int)(dmg - ((determinacao * 0.5 ) + armadura) ); //O dano é reduzido por metade da determinação , mais a armadura
        derrotado = true;
    }
}
