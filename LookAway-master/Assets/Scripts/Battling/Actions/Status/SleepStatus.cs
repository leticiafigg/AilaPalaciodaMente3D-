[System.Serializable]

public class SleepStatus : BaseStatusEffect
{
    public SleepStatus()
    {
        StatusEffectName = "Sono";
        StatusEffectDesc = "Previne que o alvo aja até acordar";
        StatusEffectID = 2;
        StatusEffectPower = 0;
        StatusEffectChance = 85; //possuirá uma chace de 75% de ser aplicado
        StatusEffectMinTurnApplied = 5;
        StatusEffectMaxTurnApplied = 6;
    }
}
