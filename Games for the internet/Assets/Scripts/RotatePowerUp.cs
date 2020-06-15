using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    private GameObject player;
    private UiInfo uiInfo;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 45f);

        if(Vector2.Distance(player.transform.position, transform.position) < 1 && uiInfo.powerUpPickUp.GetComponent<Text>().text == "")

        uiInfo.powerUpPickUp.GetComponent<Text>().text = "";


    }
}
