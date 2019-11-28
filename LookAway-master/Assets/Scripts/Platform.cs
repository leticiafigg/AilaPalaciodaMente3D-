using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform destino;

    private Vector3 target;
    private Vector3 origem;

    public float startWaitTime;
    private float waitTime;
    public float speed;
    public bool move = true;
   
    Rigidbody rdb;
    // Start is called before the first frame update
    void Start()
    {
        origem = transform.position;
        target = destino.position;
        rdb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destino.position) < 0.3f)
            {
               if (waitTime <= 0)
               {
                   waitTime = startWaitTime;
                   target = origem; 
               }
               else
               waitTime -= Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, origem) < 0.3f)
            {
                if (waitTime <= 0)
                {
                    waitTime = startWaitTime;
                    target = destino.position;
                }
                else
                waitTime -= Time.deltaTime;
            }

        }

       
     
    }


    private void OnTriggerEnter(Collider col)
    {
      
        if (col.CompareTag("Player"))
        {
           
            col.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent = null;
        }
    }


}
