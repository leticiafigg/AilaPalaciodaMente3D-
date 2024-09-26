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

    private StatCalc statcalcScript = new StatCalc();

    public void LevelUP(BasePlayer.AilaArchetype ailaClass)
    {
        GameInformation.Aila.PlayerLevel += 1;
        GameInformation.Aila.XPAtual -= GameInformation.Aila.XPNecessario; //permite que o jogador retenha algum do seu xp ao passar de nível
        
        if (ailaClass == BasePlayer.AilaArchetype.DESTEMIDA)
        {
            poderModifier = 3.0f;
            imaginacaoModifier = 1.0f;
            resistenciaModifier = 2.0f;
            determinacaoModifier = 1.0f;
            sorteModifier = 2.0f;

}
        else if (ailaClass == BasePlayer.AilaArchetype.CRIATIVA)
        {
            poderModifier = 1.0f;
            imaginacaoModifier = 3.5f;
            resistenciaModifier = 1.5f;
            determinacaoModifier = 2.0f;
            sorteModifier = 1.5f;
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

        GameInformation.Aila.XPNecessario = DeterminarXPNecessario();


        IncreaseExperience.CheckLevelUp(); //depois de fazer todo o processo de subir o nível e mudar os status de acordo, checamos novamente se o jogador ainda pode subir de nível com o xp que sobrou depois

    }

    private void AtualizarStats(int lvl) //Calcula novos status para a Aila baseado no nível recém adquirido 
    {
        int novopoder          = GameInformation.Aila.Poder;
        int novaimaginacao     = GameInformation.Aila.Imaginacao;
        int novadeterminacao   = GameInformation.Aila.Determinacao;
        int novaresistencia    = GameInformation.Aila.Resistencia;
        int novasorte          = GameInformation.Aila.Sorte;

        novopoder           = ((int)(novopoder + poderModifier * lvl));
        novaimaginacao      = ((int)(novaimaginacao + imaginacaoModifier * lvl));
        novadeterminacao    = ((int)(novadeterminacao + determinacaoModifier * lvl));
        novaresistencia     = ((int)(novaresistencia + resistenciaModifier * lvl));
        novasorte           = ((int)(novasorte + sorteModifier * lvl));

        GameInformation.Aila.Poder         = novopoder;
        GameInformation.Aila.Imaginacao    = novaimaginacao;
        GameInformation.Aila.Determinacao  = novadeterminacao;
        GameInformation.Aila.Resistencia   = novaresistencia;
        GameInformation.Aila.Sorte         = novasorte;

        GameInformation.AilaPV = statcalcScript.CalcularPV(novaresistencia);
        GameInformation.AilaPF = statcalcScript.CalcularPF(novaimaginacao);

        GameInformation.AilaPVatual = GameInformation.AilaPV; //Reinicia os valores de vida e energia para o máximo
        GameInformation.AilaPFatual = GameInformation.AilaPF;
    }

    private int DeterminarXPNecessario( )
    {
        int novoxp = GameInformation.Aila.XPNecessario * 3;

        return novoxp;
    }    

}
