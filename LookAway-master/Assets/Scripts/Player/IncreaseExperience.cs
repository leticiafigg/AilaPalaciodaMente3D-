using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseExperience 
{
    private static LevelUp lvlup;

    public static void AddExperience(int cd)  // CD = Classe de Dificuldade
    {
        int xpAward = cd * 300 ;
        GameInformation.Aila.XPAtual += xpAward;

        CheckLevelUp();

    }

    private static void CheckLevelUp()
    {
        if (GameInformation.Aila.XPAtual >= GameInformation.Aila.XPNecessario)
        {
            GameInformation.Aila.PlayerLevel += 1; //Ainda só passa de nível apenas uma vez não importando a quantidade de XP
            GameInformation.Aila.XPNecessario = GameInformation.Aila.XPNecessario * 3;
            GameInformation.Aila.XPAtual = 0;

            lvlup.LevelUP(GameInformation.Aila.PlayerLevel, GameInformation.Aila.AilaClass);
        }
    }
}
