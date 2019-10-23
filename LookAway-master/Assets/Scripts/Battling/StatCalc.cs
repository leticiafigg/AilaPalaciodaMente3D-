using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalc 
{
    private float poderModifier = 0.25f; //25%
    private float imaginacaoModifier = 0.25f;
    private float determinacaoModifier = 0.2f; // 20%
    private float sorteModifier = 0.2f;

    public enum StatType
    {
        PODER,
        IMAGINACAO,
        DETERMINACAO,
        SORTE
    }

    public int CalcularInimStats(int statVal, StatType statType, int level) //Modifica os status pessoais fornecidos de acordo com o nível e o modificador atribuído
    {
        float modifier;
        if(statType == StatType.PODER)
        {
            modifier = poderModifier;
            return (statVal + (int)(statVal + modifier * level));
        }
        if (statType == StatType.IMAGINACAO)
        {
            modifier = poderModifier;
            return (statVal + (int)(statVal + modifier * level));
        }
        if (statType == StatType.DETERMINACAO)
        {
            modifier = poderModifier;
            return (statVal + (int)(statVal + modifier * level));
        }
        if (statType == StatType.SORTE)
        {
            modifier = poderModifier;
            return (statVal + (int)(statVal + modifier * level));
        }
        return 0;
    }


    public int CalcularPV(int statValue)
    {
        int resultPV;

        resultPV = (int)(statValue * 10.5f); //simplesmente usando o valor do status e multiplicando por 10 e meio

        return resultPV;

    }

    public int CalcularPF(int statValue) 
    {
        int resultPF;

        resultPF = statValue * 4; //o mesmo que os PV, porém reduzido

        return resultPF;
    }

}
