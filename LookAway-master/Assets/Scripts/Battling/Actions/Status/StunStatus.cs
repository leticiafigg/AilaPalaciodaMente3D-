[System.Serializable]

public class StunStatus : BaseStatusEffect
{
    public StunStatus()
    {
        StatusEffectName = "Atordoamento";
        StatusEffectDesc = "Previne que o alvo aja até o turno seguinte";
        StatusEffectID = 3;
        StatusEffectPower = 0;
        StatusEffectChance = 100; 
        StatusEffectMinTurnApplied = 1;
        StatusEffectMaxTurnApplied = 1;
    }
}
