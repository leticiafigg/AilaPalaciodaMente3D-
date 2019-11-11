using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatusEffect 
{
    private string statusEffectName;
    private string statusEffectDesc;
    private int statusEffectID;
    private int statusEffectPower;
    private int statusEffectChance; //chance em porcentagem de o status acontecer
    private int statusEffectMinTurnApplied;
    private int statusEffectMaxTurnApplied;
    


    public string StatusEffectName
    {
        get { return statusEffectName; }
        set { statusEffectName = value; }
    }
    public string StatusEffectDesc
    {
        get { return statusEffectDesc; }
        set { statusEffectDesc = value; }
    }
    public int StatusEffectID
    {
        get { return statusEffectID; }
        set { statusEffectID = value; }
    }

    public int StatusEffectPower
    {
        get { return statusEffectPower; }
        set { statusEffectPower = value; }
    }

    public int StatusEffectChance
    {
        get { return statusEffectChance; }
        set { statusEffectChance = value; }
    }

    public int StatusEffectMinTurnApplied
    {
        get { return statusEffectMinTurnApplied; }
        set { statusEffectMinTurnApplied = value; }
    }

    public int StatusEffectMaxTurnApplied
    {
        get { return statusEffectMaxTurnApplied; }
        set { statusEffectMaxTurnApplied = value; }
    }



}
