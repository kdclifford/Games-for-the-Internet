using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpText : MonoBehaviour
{

    public float range;
    public LayerMask mask;
    private UiInfo uiInfo;
    private Movement playerMovement;

    private void Start()
    {
        playerMovement = gameObject.GetComponent<Movement>();
        uiInfo = GameObject.FindGameObjectWithTag("UiInfo").GetComponent<UiInfo>();
    }


    // Update is called once per frame
    void Update()
    {
            Collider2D hitColl = Physics2D.OverlapCircle(transform.position, range, mask);
        if(hitColl)
        {
            if (!playerMovement.wings && hitColl.gameObject.tag == "Wing")
            {
                uiInfo.powerUpPickUp.GetComponent<Text>().text = "Press E to Pick Up Wing Power Up";
            }
            else if (!playerMovement.shoot && hitColl.gameObject.tag == "Blob")
            {
                uiInfo.powerUpPickUp.GetComponent<Text>().text = "Press E to Pick Up Blob Power Up";
            }
            else if (!playerMovement.block && hitColl.gameObject.tag == "Block")
            {
                uiInfo.powerUpPickUp.GetComponent<Text>().text = "Press E to Pick Up Block Power Up";
            }           
        }
        else
        {
            uiInfo.powerUpPickUp.GetComponent<Text>().text = "";
        }

    }
}
