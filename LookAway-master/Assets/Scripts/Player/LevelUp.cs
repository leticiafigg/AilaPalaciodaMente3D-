using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp 
{
    private float poderModifier = 0.2f;             //Todos são 20% por padrão, até a aila ter uma "classe"
    private float imaginacaoModifier = 0.2f;
    private float determinacaoModifier = 0.2f; 
    private float resistenciaModifier = 0.2f;
    private float sorteModifier = 0.2f;

   

    public void LevelUP(BasePlayer.AilaArchetype ailaClass)
    {
        GameInformation.Aila.PlayerLevel += 1;
        GameInformation.Aila.XPAtual -= GameInformation.Aila.XPNecessario; //permite que o jogador retenha algum do seu xp ao passar de nível
        
        if (ailaClass == BasePlayer.AilaArchetype.DESTEMIDA)
        {
            poderModifier = 0.3f;
            imaginacaoModifier = 0.1f;
            resistenciaModifier = 0.3f;
            determinacaoModifier = 0.2f;
            sorteModifier = 0.2f;

}
        else if (ailaClass == BasePlayer.AilaArchetype.CRIATIVA)
        {
            poderModifier = 0.1f;
            imaginacaoModifier = 0.3f;
            resistenciaModifier = 0.2f;
            determinacaoModifier = 0.3f;
            sorteModifier = 0.2f;
        }
        else if (ailaClass == BasePlayer.AilaArchetype.AVOADA)
        {
            poderModifier = 0.2f;
            imaginacaoModifier = 0.2f;
            resistenciaModifier = 0.2f;
            determinacaoModifier = 0.2f;
            sorteModifier = 0.3f;
        }

        AtualizarStats(GameInformation.Aila.PlayerLevel);

        DeterminarXPNecessario();


        IncreaseExperience.CheckLevelUp(); //depois de fazer todo o processo de subir o nível e mudar os status de acordo, checamos novamente se o jogador ainda pode subir de nível com o xp que sobrou depois

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
        armadura        = ((int)(armadura + resistenciaModifier * lvl));
        sorte           = ((int)(sorte + sorteModifier * lvl));

        GameInformation.Aila.Poder         = poder;
        GameInformation.Aila.Imaginacao    = imaginacao;
        GameInformation.Aila.Determinacao  = determinacao;
        GameInformation.Aila.Armadura      = armadura;
        GameInformation.Aila.Sorte         = sorte;


    }

    private void DeterminarXPNecessario( )
    {
        int novoxp = GameInformation.Aila.XPNecessario * 3;
    }    

}
