using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalc 
{
    private float poderModifier = 0.25f; //25%
    private float imaginacaoModifier = 0.25f;
    private float determinacaoModifier = 0.2f; // 20%
    private float sorteModifier = 0.2f;

    private BaseAction baseactionScript;

    public enum StatType
    {
        PODER,
        IMAGINACAO,
        DETERMINACAO,
        SORTE
    }

    

   /* public enum AilaArchetype // Estes arquétipos irão determinar os modificadores do jogador
    {
        CONSCIENTE,
        IMAGINATIVA,
        AVOADA
    }*/

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

        resultPV = (int)(statValue * 10); //simplesmente usando o valor do status e multiplicando por 10 

        return resultPV;

    }

    public int CalcularPF(int statValue) 
    {
        int resultPF;

        resultPF = statValue * 4; //o mesmo que os PV, porém reduzido

        return resultPF;
    }


    public int GetActionAffinity(StatType statAff)
    {
        float Affnitymodifier = 0.5f;

        if (statAff == StatType.PODER)
        {          
            return  (int)(GameInformation.Aila.Poder * Affnitymodifier);
        }
        if (statAff == StatType.IMAGINACAO)
        {    
            return (int)(GameInformation.Aila.Imaginacao * Affnitymodifier);
        }
        if (statAff == StatType.DETERMINACAO)
        {        
           return  (int)(GameInformation.Aila.Determinacao * Affnitymodifier);
        }
        if (statAff == StatType.SORTE)
        {
            return  (int)(GameInformation.Aila.Sorte * Affnitymodifier);
        }

        return 1;
    }
    
}
