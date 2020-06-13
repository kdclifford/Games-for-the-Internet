using System.Collections;
using System.Collections.Generic;
using AiAnimationController.Utils;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Collider2D agentCol;
    private Rigidbody2D agentRig;
    private float flyAmount;
    private Vector3 startPos;
    public GameObject player;
    public Animator agentAnim;
    public GameObject attackBox;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        agentCol = GetComponent<Collider2D>();
        agentRig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AiAnimations.Walk(agentAnim);
        float playDist = Vector2.Distance(player.transform.position, transform.position);
        if (playDist < 2)
        {
            AiAnimations.Attack(agentAnim);
            attackBox.SetActive(true);
            //attackBox.SetActive(false);
        }
           else if (playDist < 5)
        {
            float dotProd = player.transform.position.x - transform.position.x;
            if (dotProd > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (dotProd < 0)
            {

                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

            }


            Vector2 playerDirection = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerDirection.Normalize();
            agentRig.AddForce(playerDirection * playDist + new Vector2(0, 2));
        }
        else
        {
          float startDist = Vector3.Distance(startPos, transform.position);
            Vector2 startDirection = new Vector2(startPos.x - transform.position.x, startPos.y - transform.position.y);
            startDirection.Normalize();
            if (startDist > 10)
            {
                float dotProd = startPos.x - transform.position.x;
                if (dotProd > 0)
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }
                else if (dotProd < 0)
                {

                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

                }
                agentRig.AddForce(startDirection * (startDist * 0.5f));
            }
            else
            {
                if (transform.position.y > startPos.y + 1)
                {
                    flyAmount = 1;
                }
                else
                {
                    flyAmount = 5;
                }
                agentRig.AddForce(new Vector2(0, flyAmount));
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
}
