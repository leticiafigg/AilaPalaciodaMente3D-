using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp 
{
    private float poderModifier = 0.2f;             //Todos são 10% por padrão, até a aila ter uma "classe"
    private float imaginacaoModifier = 0.2f;
    private float determinacaoModifier = 0.2f; 
    private float armaduraModifier = 0.2f;
    private float sorteModifier = 0.2f;

   

    public void LevelUP(int level, string ailaClass)
    {
        if(ailaClass == "Corajosa")
        {
            poderModifier = 0.3f;
            armaduraModifier = 0.25f;
            
        }
        else if (ailaClass == "Criativa")
        {
            imaginacaoModifier = 0.3f;
            determinacaoModifier = 0.25f;

        }
        else if (ailaClass == "Avoada")
        {
            sorteModifier = 0.3f;

        }

        AtualizarStats(level);

    }

    private void AtualizarStats(int lvl)
    {
        int poder          = GameInformation.Aila.Poder;
        int imaginacao     = GameInformation.Aila.Imaginacao;
        int determinacao   = GameInformation.Aila.Determinacao;
        int armadura       = GameInformation.Aila.Armadura;
        int sorte          = GameInformation.Aila.Sorte;

        poder           = ((int)(poder + poderModifier * lvl));
        imaginacao      = ((int)(imaginacao + imaginacaoModifier * lvl));
        determinacao    = ((int)(determinacao + determinacaoModifier * lvl));
        armadura        = ((int)(armadura + armaduraModifier * lvl));
        sorte           = ((int)(sorte + sorteModifier * lvl));

        GameInformation.Aila.Poder         = poder;
        GameInformation.Aila.Imaginacao    = imaginacao;
        GameInformation.Aila.Determinacao  = determinacao;
        GameInformation.Aila.Armadura      = armadura;
        GameInformation.Aila.Sorte         = sorte;


    }


}
