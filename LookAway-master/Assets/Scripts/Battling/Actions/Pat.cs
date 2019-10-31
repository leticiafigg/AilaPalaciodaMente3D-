[System.Serializable]

public class Pat : BaseAction
{
    public Pat()
    {
        
        ActionName = "Cafuné";
        ActionDesc = "Um gentil cafuné, pode causar sono";
        ActionID = 3;
        ActionPower = 0;
        StunPower = 1;
        ActionCost = 0;

        ActionEffects.Add(new SleepStatus());

        StatAffinity = StatCalc.StatType.SORTE;

    }

}
