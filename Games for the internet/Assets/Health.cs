using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
    public LayerMask projectile;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.layer == 16 && collision.gameObject.GetComponent<HitOnce>().destroy == false)
        {
            collision.gameObject.GetComponent<HitOnce>().destroy = true;
            Destroy(collision.gameObject);
            currentHealth--;
        }
        else if (collision.gameObject.layer == 17 && collision.gameObject.GetComponent<HitOnce>().destroy == false)
        {
            collision.gameObject.GetComponent<HitOnce>().destroy = true;

            if(collision.gameObject.tag == "Wings")
            {
                
            }


            Destroy(collision.gameObject);


        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
