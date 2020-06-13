using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManger : MonoBehaviour
{
    private Movement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    void wingPowerUp()
    {
        playerMovement.wings = true;
        playerMovement.block = false;
        playerMovement.shoot = false;
    }

    void blockPowerUp()
    {
        playerMovement.wings = false;
        playerMovement.block = true;
        playerMovement.shoot = false;
    }

    void shootPowerUp()
    {
        playerMovement.wings = false;
        playerMovement.block = false;
        playerMovement.shoot = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
