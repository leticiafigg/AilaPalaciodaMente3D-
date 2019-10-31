using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatus : BaseStatusEffect
{
    public BurnStatus()
    {
        StatusEffectName = "Queimadura";
        StatusEffectDesc = "Causa danos ao longo de cada turno";
        StatusEffectID = 1;
        StatusEffectPower = 15;
        StatusEffectChance = 75; //possuirá uma chace de 75% de ser aplicado
        StatusEffectMinTurnApplied = 4;
        StatusEffectMaxTurnApplied = 6;
    }

   
}
