﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPatrol : MonoBehaviour
{
    public Animator anim;
    private float animspeed;

    public float speed;
    public float chaseSpeed;

    private float chaseTime;
    private float waitTime;

    public float startWaitTime;
    public float startChaseTime;

    private bool foraDeAlcance;

    public Transform[] pontosDePatrulha; //Pontos no espaço em que a IA vai tentar alcançar
    private Transform target;
    private float permanentY;

    private int randomPonto;

    public enum EstadoDePatrulha
    {
        PATRULHANDO,
        PERSEGUINDO
    }

    public EstadoDePatrulha estadoatual;

    // Start is called before the first frame update
    void Start()
    {
        if(GameInformation.LastEnemy == this.gameObject.name)
        {
            Destroy(this.gameObject);
        }


        permanentY = transform.position.y;
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
                animspeed = 1.0f;
                anim.SetFloat("animSpeed", animspeed);
                PatrulharPontos();
                break;

            case (EstadoDePatrulha.PERSEGUINDO):
                animspeed = 2.0f;
                anim.SetFloat("animSpeed", animspeed) ;
                PerseguirJogador();
                break;

        }

    }

    private void PatrulharPontos()
    {
        target = pontosDePatrulha[randomPonto];
       

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, permanentY, transform.position.z);
        transform.LookAt(target.position);

        anim.SetBool("walking", true);

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
                anim.SetBool("walking", false);
            }

        }
    }

    private void PerseguirJogador()
    {
        speed = chaseSpeed;
        anim.SetBool("walking", true);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, permanentY, transform.position.z);

        transform.LookAt(target);

        if (foraDeAlcance) //Se o jogador fica fora do trigger que detecta sua presença por um certo tempo, o inimigo volta a patrulhar
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
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
