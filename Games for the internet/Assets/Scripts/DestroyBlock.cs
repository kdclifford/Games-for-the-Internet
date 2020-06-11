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
    public bool die = false;

    private GameObject uiInfo;
    private bool spawnOnce = false;
    public void Start()
    {
        uiInfo = GameObject.FindGameObjectWithTag("UiInfo");
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


        if (collision.gameObject.tag == "AttackBox" && collision.gameObject.layer == 9)
        {
            //Debug.Log(collision.name);

            TakeDamage();
          
          

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

    public void TakeDamage()
    {
        uiInfo.GetComponent<UiInfo>().score.GetComponent<ScoreScript>().AddScore(1);
        agentInfo.health--;
        sRend.material = whiteMat;
        Invoke("ResetMat", 0.2f);
    }

    private void Update()
    {
        if (die)
        {
            Destroy(gameObject);
            GameObject broken = (GameObject)Instantiate(brokenPrefab);

            //Set position of barrel
            broken.transform.position = transform.position;
            broken.transform.rotation = transform.rotation;
            broken.transform.localScale = transform.localScale;
            spawnOnce = true;
        }

        if (agentInfo.health <= 0 && !spawnOnce)
        {
            Destroy(gameObject);
            GameObject broken = (GameObject)Instantiate(brokenPrefab);

            //Set position of barrel
            broken.transform.position = transform.position;
            broken.transform.rotation = transform.rotation;
            broken.transform.localScale = transform.localScale;
            spawnOnce = true;


        }


    }

}
