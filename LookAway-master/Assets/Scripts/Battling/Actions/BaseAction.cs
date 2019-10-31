using System.Collections.Generic;

[System.Serializable]

public class BaseAction 
{
    private string actionName; //Identificador por escrito do movimento
    private string actionDesc; //Descrição breve da ação, para o jogador entender o que faz
    private int actionID; 
    private int actionPower; //Número a ser escalonado posteriormente, dependendo do status associado e seu efeito (Ex: Dano a ser causado)
    private int stunPower;
    private int actionCost; //Custo de recurso para o utilizador, tornar 0 caso não tenha custo.
    private List<BaseStatusEffect> actionEffects = new List<BaseStatusEffect>();
    

    private StatCalc.StatType statAffinity;

    public StatCalc.StatType StatAffinity
    {
        get { return statAffinity; }
        set { statAffinity = value; }

    }

    public string ActionName
    {
        get { return actionName; }
        set { actionName = value; }
    }
    public string ActionDesc
    {
        get { return actionDesc; }
        set { actionDesc = value; }
    }
    public int ActionID
    {
        get { return actionID; }
        set { actionID = value; }
    }

    public int ActionPower
    {
        get { return actionPower; }
        set { actionPower = value; }
    }

    public int StunPower
    {
        get { return stunPower; }
        set { stunPower = value; }
    }

    public int ActionCost
    {
        get { return actionCost; }
        set { actionCost = value; }
    }

    public List<BaseStatusEffect> ActionEffects
    {
        get { return actionEffects; }
        set { actionEffects = value; }

    }

}
