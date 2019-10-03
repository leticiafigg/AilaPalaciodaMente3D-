using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer 
{
    private string playerName;
   // private PlayerClasses basePlayerClass;
    private int playerLevel;
    private int poder;        // valor total que determina quantos pontos de dano físico são ccausados com ataques
    private int imaginacao;   // valor total que determina quantos PF o personagem possui, e quão boas são suas "Magias"
    private int determinacao; // Valor total que determina quanta vida e resistência a efeitos incomuns
    private int armadura;     // Valor total que determina a redução do dano recebido (nunca reduzirá um valor a 0)
    private int sorte;        // Valor total que determina a chance de causar um crítico, o dano do crítico, ou evasão(Reduz um dano a 0)

    private int xpatual;
    private int xpnecessario;

    public string PlayerName { get; set; } 
    public int XPAtual { get; set;}
    public int XPNecessario { get; set; }  //faz basicamente o que os de baixo fazem.


    public int PlayerLevel
    {
        get { return playerLevel; }
        set { playerLevel = value; }
    }

  /*public PlayerClasses BasePlayerClass
    {
        get { return basePlayerClass; }
        set { basePlayerClass = value; }
    }*/

    public int Poder
    {
        get { return poder; }
        set { poder = value; }
    }

    public int Imaginacao
    {
        get { return imaginacao; }
        set { imaginacao = value; }
    }

    public int Determinacao
    {
        get { return determinacao; }
        set { determinacao = value; }
    }

    public int Armadura
    {
        get { return armadura; }
        set { armadura = value; }
    }

    public int Sorte
    {
        get { return sorte; }
        set { sorte = value; }
    }



}


