using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAcidBlob : MonoBehaviour
{
    //Direction the blob willl travel
    public int direction;
    private Rigidbody2D agentRig;
    private AudioManager audio;

    void Start()
    {
        agentRig = GetComponent<Rigidbody2D>();
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audio.Play("BlobShot", gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        agentRig.velocity = (new Vector2(6 * direction, 0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 16)
        {
            if(gameObject.layer == 9 &&  collision.gameObject.layer == 20 | collision.gameObject.layer == 21 | collision.gameObject.layer == 22 )
            {
                collision.gameObject.GetComponent<DestroyBlock>().TakeDamage();
                Destroy(gameObject);
            }

           else if (collision.gameObject.layer != 9)
            {
                Debug.Log(collision.gameObject.name);
                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Health>().currentHealth--;
                Debug.Log(collision.gameObject.name);
                Destroy(gameObject);
            }
        }
    }


}
