using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : BaseItemStat
{
 
    public enum WeaponTypes
    {
        SWORD,
        STAFF,
        DAGGER,
        BOW,
        SHIELD,
        POLEARM
    }

    private WeaponTypes weaponType;
    //private int Effect;  - Possível efeito a ser adicionado no futuro

    public WeaponTypes WeaponType
    {
        get { return weaponType;}
        set { weaponType = value;}
    }

}
