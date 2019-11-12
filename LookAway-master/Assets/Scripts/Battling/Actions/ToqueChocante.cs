[System.Serializable]

public class ToqueChocante : BaseAction
{
    public ToqueChocante()
    {
        ActionName = "Toque Chocante";
        ActionDesc = "Eletricidade estática envolve as mãos do usuário. Tocar o inimigo causa bastante dano, e médio atordoamento";
        ActionID = 2;
        ActionPower = 2;
        StunPower = 30;
        ActionCost = 8;
        ActionCritChance = 20;

        StatAffinity = StatCalc.StatType.IMAGINACAO;

    }


}
