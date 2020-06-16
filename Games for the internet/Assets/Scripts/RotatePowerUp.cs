using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    public GameObject player;
    private UiInfo uiInfo;
    private bool once = false;
    // Start is called before the first frame update
    void Start()
    {
        uiInfo = GameObject.FindGameObjectWithTag("UiInfo").GetComponent<UiInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!once)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            once = true;
        }
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 45f);

        //if (Vector2.Distance(player.transform.position, transform.position) < 1)
        //{
        //    uiInfo.powerUpPickUp.GetComponent<Text>().text = "";
        //}

    }
}
