using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    // Start is called before the first frame update

    public Object brokenPrefab;
    public Color colour1;

    public Color colour2;

    public int health = 3;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health--;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            GameObject broken = (GameObject)Instantiate(brokenPrefab);

            //Set position of barrel
            broken.transform.position = transform.position;
            broken.transform.rotation = transform.rotation;
            //Destory old barrel
            Destroy(gameObject);

        }
       else if (health == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().color = colour1;
        }

        else if (health == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = colour2;
        }

    }
}
