using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    // Start is called before the first frame update

    private AiAgentInfo agentInfo;
    public Object brokenPrefab;
    private Color colour1 = new Color(0.8f, 0.8f, 0.8f, 1.0f);
    private Color colour2 = new Color(0.5f, 0.5f, 0.5f, 1.0f);

    private Material whiteMat;
    private Material defaultMat;

    private SpriteRenderer sRend;

    private int health = 0;

    public void Start()
    {
        sRend = GetComponent<SpriteRenderer>();
        whiteMat = Resources.Load("BrightWhite", typeof(Material)) as Material;
        defaultMat = sRend.material;
        agentInfo = GetComponent<AiAgentInfo>();
        health = agentInfo.health;
    }

    public void ResetMat()
    {
        sRend.material = defaultMat;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.layer == 12)
        {
            Debug.Log(collision.name);
            agentInfo.health--;
            sRend.material = whiteMat;
            Invoke("ResetMat", 0.2f);
            if (agentInfo.health <= 0)
            {
                GameObject broken = (GameObject)Instantiate(brokenPrefab);

                //Set position of barrel
                broken.transform.position = transform.position;
                broken.transform.rotation = transform.rotation;
                broken.transform.localScale = transform.localScale;
                //Destory old barrel
                Destroy(gameObject);

            }

        }
        //else if (health == 2)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().color = colour1;
        //}

        //else if (health == 1)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().color = colour2;
        //}


    }
}
