using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
    public LayerMask projectile;
    public Movement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<Movement>();
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        
        if (collision.gameObject.layer == 16)
            {
                // collision.gameObject.GetComponent<HitOnce>().destroy = true;
                playerMovement.IsHit();
                playerMovement.HitAnimation();
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);
                currentHealth--;
            }
            else if (collision.gameObject.layer == 18 && collision.gameObject.activeSelf == true)
            {
                playerMovement.IsHit();
                playerMovement.HitAnimation();
                currentHealth--;
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


            }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
