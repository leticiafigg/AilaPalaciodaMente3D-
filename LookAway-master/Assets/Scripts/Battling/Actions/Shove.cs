﻿[System.Serializable]

public class Shove : BaseAction
{
    public Shove()
    {
        ActionName = "Empurrar";
        ActionDesc = "Avança com a intenção de desequilibrar o oponente. Dano reduzido, mas com maior Atordoamento";
        ActionID = 2;
        ActionPower = 5;
        StunPower = 40;
        ActionCost = 0;
        ActionCritChance = 35;

        StatAffinity = StatCalc.StatType.PODER;

    }

}
