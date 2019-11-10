using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionChoice 
{
    private BaseAction AcaoEscolhida;

    public BaseAction ChooseEnemyAction(Inimigo inim)
    {
        switch (inim.EstadoAtual)
        {
            case (Inimigo.EnemyState.BEM):
                AcaoEscolhida = new AttackAction();
                break;
            case (Inimigo.EnemyState.AGRESSIVO):
                AcaoEscolhida = new AttackAction();
                break;
            case (Inimigo.EnemyState.MORRENDO):
                AcaoEscolhida = new Defender(inim);
                break;
        }

        inim.Agiu = true;
        Debug.Log(inim.name + " usou " + AcaoEscolhida.ActionName);
        return AcaoEscolhida;
    }
}
