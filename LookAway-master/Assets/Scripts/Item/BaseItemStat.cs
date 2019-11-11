using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemStat : BaseItem
{
    private int poder;
    private int imaginacao;
    private int determinacao;
    private int armadura;
    private int sorte;


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
