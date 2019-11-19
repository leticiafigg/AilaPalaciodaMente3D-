using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBounds : MonoBehaviour
{
    public EnemyPatrol patrulheiro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (patrulheiro != null)
        {
            if (patrulheiro.gameObject.name == other.gameObject.name) //se quem saiu da area do trigger/patrulha foi o próprio inimigo (Especificamente por perseguir o player) ele retorna ao estado de patrulha
            {
                patrulheiro.estadoatual = EnemyPatrol.EstadoDePatrulha.PATRULHANDO;
            }
        }
    }
}
