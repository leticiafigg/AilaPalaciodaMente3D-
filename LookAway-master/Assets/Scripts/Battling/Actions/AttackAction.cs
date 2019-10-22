[System.Serializable]

public class AttackAction : BaseAction
{
  public AttackAction()
    {
        int StunPower; //A maioria dos ataques causará algum dano de stun

        ActionName = "Ataque Basico";
        ActionDesc = "Um ataque simples. Dano moderado, baixo Atordoamento";
        ActionID = 1 ;
        ActionPower= 5 ;
        StunPower = 10 ;
        ActionCost = 0;
    }

}
