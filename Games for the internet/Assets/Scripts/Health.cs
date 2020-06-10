using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
    public LayerMask projectile;
    public Movement playerMovement;
    private GameObject uiInfo;

    // Start is called before the first frame update
    void Start()
    {
        uiInfo = GameObject.FindGameObjectWithTag("UiInfo");
        playerMovement = gameObject.GetComponent<Movement>();
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.name);

        if (collision.gameObject.tag == "AttackBox" && collision.gameObject.layer == 14)
        {

            // collision.gameObject.GetComponent<HitOnce>().destroy = true;
            playerMovement.IsHit();
            playerMovement.HitAnimation();
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            currentHealth--;
            uiInfo.GetComponent<UiInfo>().score.GetComponent<ScoreScript>().AddScore(-1);
        }
        else if (collision.gameObject.layer == 18 && collision.gameObject.activeSelf == true)
        {
            playerMovement.IsHit();
            playerMovement.HitAnimation();
            currentHealth--;
            uiInfo.GetComponent<UiInfo>().score.GetComponent<ScoreScript>().AddScore(-1);
            // collision.gameObject.SetActive(false);            
        }
        else if (collision.gameObject.layer == 17)
        {
            //  collision.gameObject.GetComponent<HitOnce>().destroy = true;

            if (collision.gameObject.tag == "Wing")
            {
                Debug.Log("wings");
            }
            else if (collision.gameObject.tag == "Blob")
            {
                Debug.Log("Blob");
            }



            Destroy(collision.gameObject);
            uiInfo.GetComponent<UiInfo>().score.GetComponent<ScoreScript>().AddScore(3);


        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}
