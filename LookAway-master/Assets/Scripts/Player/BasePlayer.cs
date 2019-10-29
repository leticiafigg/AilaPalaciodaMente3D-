using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer 
{
    private string playerName;
   // private PlayerClasses basePlayerClass;
    private int playerLevel;
    private int poder;        // valor total que determina quantos pontos de dano físico são causados com ataques
    private int imaginacao;   // valor total que determina quantos PF o personagem possui, e quão boas são suas "Magias"
    private int determinacao; // Valor total que determina quanta vida e resistência a efeitos incomuns
    private int armadura;     // Valor total que determina a redução do dano recebido (nunca reduzirá um valor a 0)
    private int sorte;        // Valor total que determina a chance de causar um crítico, o dano do crítico, ou evasão(Reduz um dano a 0)

    public enum AilaArchetype
    {
        CORAJOSA,           //Descritor :Aumenta o Poder++ e Armadura+ - Aila é corajosa e gosta de tomar a iniciativa, resolve os conflitos com as próprias mãos, difícil de derrotar
        CRIATIVA,           //Descritor :Aumenta a Imaginação++ e Determinação+ - Aila é criativa, então copnsegue imaginar mlehor formas mais incovenientes e poderosas de lidar com oponentes
        AVOADA              //Descritor :Aumenta a Sorte++ - Aila permite que as coisas tomem o próprio rumo, pois acredita que tem sorte grande. Acertos críticos dela ou erros de inimigos são bem mais frequentes
    }


    private string ailaClass;
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

    public string AilaClass
    {
        get { return ailaClass; }
        set { ailaClass = value; }
    }

    public string AilaArch(AilaArchetype ailaArch)
    {

        if (ailaArch == AilaArchetype.CORAJOSA)
        {
            return "Corajosa";
        }
        if (ailaArch == AilaArchetype.CRIATIVA)
        {
            return "Criativa";
        }
        if (ailaArch == AilaArchetype.AVOADA)
        {
            return "Avoada";
        }

        return "Corajosa";
    }

}


