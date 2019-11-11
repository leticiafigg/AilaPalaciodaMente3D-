using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IncreaseExperience 
{
    private static LevelUp lvlupScript = new LevelUp();
    private static int xpReceber;

    public static void AddExperience(int cd)  // CD = Classe de Dificuldade
    {
        xpReceber = cd * 300 ;
        GameInformation.Aila.XPAtual += xpReceber;

        CheckLevelUp();

    }

    public static void CheckLevelUp()
    {
        if (GameInformation.Aila.XPAtual >= GameInformation.Aila.XPNecessario) //Checa se o jogador passou de nível com seu xp atual, e se passou ele aumenta o nível, muda o xp necessário, e então chama a função de LevelUp do script
        { 
           lvlupScript.LevelUP(GameInformation.Aila.AilaClass);
        }
    }
}
