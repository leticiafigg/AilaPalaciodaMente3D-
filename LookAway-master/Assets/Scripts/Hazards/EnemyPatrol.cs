using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;
    public float chaseSpeed;

    private float chaseTime;
    private float waitTime;

    public float startWaitTime;
    public float startChaseTime;

    private bool foraDeAlcance;

    public Transform[] pontosDePatrulha; //Pontos no espaço em que a IA vai tentar alcançar
    private Transform target;

    private int randomPonto;

    public enum EstadoDePatrulha
    {
        PATRULHANDO,
        PERSEGUINDO
    }

    private EstadoDePatrulha estadoatual;

    // Start is called before the first frame update
    void Start()
    {
        estadoatual = EstadoDePatrulha.PATRULHANDO;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        waitTime = startWaitTime;
        randomPonto = Random.Range(0, pontosDePatrulha.Length); 
    }

    // Update is called once per frame
    void Update()
    {
        

        switch(estadoatual)
        {
            case (EstadoDePatrulha.PATRULHANDO):
                PatrulharPontos();
                break;

            case (EstadoDePatrulha.PERSEGUINDO):

                PerseguirJogador();
                break;

        }

    }

    private void PatrulharPontos()
    {
        transform.position = Vector3.MoveTowards(transform.position, pontosDePatrulha[randomPonto].position, speed * Time.deltaTime);
        transform.LookAt(pontosDePatrulha[randomPonto]);
        if (Vector3.Distance(transform.position, pontosDePatrulha[randomPonto].position) < 0.3f)
        {
            if (waitTime <= 0)
            {
                randomPonto = Random.Range(0, pontosDePatrulha.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }

    private void PerseguirJogador()
    {
        speed = chaseSpeed;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target);
        if (foraDeAlcance) //
        {
            if (chaseTime <= 0)
            {
                estadoatual = EstadoDePatrulha.PATRULHANDO;
                chaseTime = startChaseTime;
            }
            else
            {
                chaseTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            foraDeAlcance = false;
            estadoatual = EstadoDePatrulha.PERSEGUINDO;
            chaseTime = startChaseTime; //Sempre que o jogador entrar no "Campo de visão" reseta o chase time para o inicial
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foraDeAlcance = true;
        }
    }
}
