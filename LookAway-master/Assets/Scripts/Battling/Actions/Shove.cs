[System.Serializable]

public class Shove : BaseAction
{
    public Shove()
    {
        int StunPower;

        ActionName = "Empurrão";
        ActionDesc = "Avança com a intenção de desequilibrar o oponente. Dano reduzido, mas com maior Atordoamento";
        ActionID = 2;
        ActionPower = 2;
        StunPower = 35;
        ActionCost = 0;
    }

}
