[System.Serializable]

public class Zonzar : BaseAction
{
    public Zonzar()
    {
        ActionName = "Zonzar";
        ActionDesc = "Não faz nada...Passa o turno";
        ActionID = 0;
        ActionPower = 0;
        StunPower = 0;
        ActionCost = 0;
        ActionCritChance = 0;

        StatAffinity = StatCalc.StatType.SORTE;
    }
}
