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

    public int CalcularStats(int statVal, StatType statType, int level)
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


}
