[System.Serializable]

public class AttackAction : BaseAction
{
  public AttackAction()
    {
        //A maioria dos ataques causará algum dano de stun
        ActionName = "Investida";
        ActionDesc = "Um ataque simples. Dano moderado, baixo Atordoamento";
        ActionID = 1 ;
        ActionPower= 10 ;
        StunPower = 10 ;
        ActionCost = 0;

        StatAffinity = StatCalc.StatType.PODER; 

    }

}
