using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : BaseAction
{
    public Defender(Inimigo inimstats)
    {
        //A maioria dos ataques causará algum dano de stun
        ActionName = "Defesa";
        ActionDesc = "Faz de tudo para reduzir o dano recebido, não causa dano";
        ActionID = 3;
        ActionPower = 0;
        StunPower = 0;
        ActionCost = 0;
        ActionCritChance = 0;

        StatAffinity = StatCalc.StatType.DETERMINACAO;

        inimstats.armadura += 5;

    }
}
