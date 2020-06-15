using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
    public LayerMask projectile;
    private Movement playerMovement;
    private UiInfo uiInfo;
    private PowerUpManger managerPowerUp;
    bool clearPowerUpText = false;

    // Start is called before the first frame update
    void Start()
    {
        uiInfo = GameObject.FindGameObjectWithTag("UiInfo").GetComponent<UiInfo>();
        playerMovement = gameObject.GetComponent<Movement>();
        currentHealth = startingHealth;
        managerPowerUp = GetComponent<PowerUpManger>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 17)
        {
            //  collision.gameObject.GetComponent<HitOnce>().destroy = true;
            if (!playerMovement.wings && collision.gameObject.tag == "Wing")
            {                
                if (Input.GetKey(KeyCode.E))
                {
                    managerPowerUp.wingPowerUp();
                    //DestroyPowerUP(3, collision.gameObject);
                }
            }
            else if (!playerMovement.shoot && collision.gameObject.tag == "Blob")
            {               
                if (Input.GetKey(KeyCode.E))
                {
                    managerPowerUp.shootPowerUp();
                    //DestroyPowerUP(3, collision.gameObject);
                }
            }
            else if (!playerMovement.block && collision.gameObject.tag == "Block")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    managerPowerUp.blockPowerUp();
                   // DestroyPowerUP(3, collision.gameObject);
                }
            }
        }
       
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
    }

    void DestroyPowerUP(int score, GameObject powerUp)
    {
        //Destroy(powerUp);
        uiInfo.GetComponent<UiInfo>().score.GetComponent<ScoreScript>().AddScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        // if(clearPowerUpText)
        //{
        //    uiInfo.powerUpPickUp.GetComponent<Text>().text = "";
        //}
        //clearPowerUpText = true;
    }
}
