using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalc 
{
    private float poderModifier = 0.25f; //25%
    private float imaginacaoModifier = 0.25f;
    private float resistenciaModifier = 0.20f;
    private float determinacaoModifier = 0.2f; // 20%
    private float sorteModifier = 0.2f;
    

    //private BaseAction baseactionScript;

    public enum StatType
    {
        PODER,
        IMAGINACAO,
        DETERMINACAO,
        RESISTENCIA,
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
            return (statVal + (int)(statVal + modifier * level/2));
        }
        if (statType == StatType.IMAGINACAO)
        {
            modifier = imaginacaoModifier;
            return (statVal + (int)(statVal + modifier * level/2));
        }
        if (statType == StatType.RESISTENCIA)
        {
            modifier = resistenciaModifier;
            return (statVal + (int)(statVal + modifier * level/2));
        }
        if (statType == StatType.DETERMINACAO)
        {
            modifier = determinacaoModifier;
            return (statVal + (int)(statVal + modifier * level/2));
        }
        if (statType == StatType.SORTE)
        {
            modifier = sorteModifier;
            return (statVal + (int)(statVal + modifier * level/2));
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


    public float GetActionAffinity(StatType statAff)
    {
        float Affnitymodifier = 0.6f;

        if (statAff == StatType.PODER)
        {          
            return  (GameInformation.Aila.Poder * Affnitymodifier);
        }
        if (statAff == StatType.IMAGINACAO)
        {    
            return (GameInformation.Aila.Imaginacao * Affnitymodifier);
        }
        if (statAff == StatType.RESISTENCIA)
        {
            return (GameInformation.Aila.Resistencia * Affnitymodifier);
        }
        if (statAff == StatType.DETERMINACAO)
        {        
           return  (GameInformation.Aila.Determinacao * Affnitymodifier);
        }
        if (statAff == StatType.SORTE)
        {
            return  (GameInformation.Aila.Sorte * Affnitymodifier);
        }

        return 1;
    }

    public float GetEnemyActionAffinity(StatType statAff , Inimigo inim)
    {
        float Affnitymodifier = 0.20f;

        if (statAff == StatType.PODER)
        {
            return (inim.poder * Affnitymodifier);
        }
        if (statAff == StatType.IMAGINACAO)
        {
            return (inim.imaginacao * Affnitymodifier);
        }
        if(statAff == StatType.RESISTENCIA)
        {
            return (inim.resistencia * Affnitymodifier);
        }
        if (statAff == StatType.DETERMINACAO)
        {
            return (inim.determinacao * Affnitymodifier);
        }
        if (statAff == StatType.SORTE)
        {
            return (inim.sorte * Affnitymodifier);
        }

        return 1;
    }

}
