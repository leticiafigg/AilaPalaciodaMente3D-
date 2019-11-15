using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject powerUpGmObj;
    public PowerUps powerUp;
    public float startingRespwanTime;
    private float respawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(powerUp.coletado)
        {
            powerUpGmObj.SetActive(false);
            respawnTime -= Time.deltaTime;

            if(respawnTime <= 0)
            {
                RespawnPowerUp();
            }
        }
    }

    public void RespawnPowerUp()
    {
        powerUpGmObj.SetActive(true);
        powerUp.coletado = false;
        respawnTime = startingRespwanTime;
    }

}
