[System.Serializable]

public class ToqueChocante : BaseAction
{
    public ToqueChocante()
    {
        ActionName = "Toque Chocante";
        ActionDesc = "Toca o inimigo toca o inimigo com mão eletrizadas, atordoamento considerável";
        ActionID = 5;
        ActionPower = 30;
        StunPower = 30;
        ActionCost = 8;
        ActionCritChance = 20;

        StatAffinity = StatCalc.StatType.IMAGINACAO;

    }


}
