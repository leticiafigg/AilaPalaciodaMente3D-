using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{


    public GameObject cameraPos;

    public int pvTotal; //cada inimigo tem um nome para identificá-lo, uma quantidade de vida e de stun
    public int stunTotal;
    public int pvAtual;
    public int stunAtual;

    private string nome;
    private int enemylvl;
    public int maxlvl;     //Status gerais (Exceto a Armadura) mudarão de acordo com o nível do inimigo,
    public int poder;      // - mas serão baseados numa predefinição dada no prefab, para facilitar implementação
    public int imaginacao;
    public int resistencia;
    public int determinacao;
    public int armadura;   // A armadura é específica para cada tipo de inimigo
    public int sorte;

    public List<BaseAction> moveList;
    public int PosInList; //O próprio Inimigo salva a sua posição no array criado com BattleHandler>BattleStart

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

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }


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
        if (this.pvAtual <= 0 && !derrotado)
        {

            if (BattleHandler.inimigosList.Count > 0)
            {
                BattleHandler.inimigosList.Remove(this); // quando morre é retirado da lista(por precaução) e também destrói o gameobject
                BattleHandler.inimObjList.Remove(this.gameObject);
            }

            derrotado = true;

            if(BattleHandler.inimigosList.Count == 0)  //Toda vez que um inimigo morrer ele checa se há outro inimigo na lista, e se ela estiver vazia, o jogador venceu
            {
                BattleHandler.currentState = BattleHandler.BattleStates.WIN;
            }

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
       pvAtual = pvAtual - dmg; 
    }

    public void TakeDamage(int dmg , int stun)
    {
        pvAtual = pvAtual - dmg;

        stunAtual = stunAtual + stun;
    }
}
