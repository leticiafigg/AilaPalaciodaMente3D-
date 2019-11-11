[System.Serializable]

public class Shove : BaseAction
{
    public Shove()
    {
        ActionName = "Empurrão";
        ActionDesc = "Avança com a intenção de desequilibrar o oponente. Dano reduzido, mas com maior Atordoamento";
        ActionID = 2;
        ActionPower = 2;
        StunPower = 30;
        ActionCost = 0;
        ActionCritChance = 85;

        StatAffinity = StatCalc.StatType.PODER;

    }

}
