using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class PowerUpManger : MonoBehaviour
{
    private Movement playerMovement;
    private UiInfo uiInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
        uiInfo = GameObject.FindGameObjectWithTag("UiInfo").GetComponent<UiInfo>();
    }

  public void wingPowerUp()
    {
        playerMovement.wings = true;
        playerMovement.block = false;
        playerMovement.shoot = false;
        uiInfo.powerUpWings.SetActive(true);
        uiInfo.powerUpBlock.SetActive(false);
        uiInfo.powerUpBlob.SetActive(false);
    }

    public void blockPowerUp()
    {
        playerMovement.wings = false;
        playerMovement.block = true;
        playerMovement.shoot = false;
        uiInfo.powerUpWings.SetActive(false);
        uiInfo.powerUpBlock.SetActive(true);
        uiInfo.powerUpBlob.SetActive(false);
    }

    public void shootPowerUp()
    {
        playerMovement.wings = false;
        playerMovement.block = false;
        playerMovement.shoot = true;
        uiInfo.powerUpWings.SetActive(false);
        uiInfo.powerUpBlock.SetActive(false);
        uiInfo.powerUpBlob.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
