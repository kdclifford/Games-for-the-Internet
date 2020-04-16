using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private SpriteRenderer PlayerSprite;
    public Animator CurrentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


        if (!CurrentAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                FlipDirectionRight();
                transform.position = new Vector3(transform.position.x + (3 * Time.deltaTime), transform.position.y, transform.position.z);
                MoveAnimation();
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                FlipDirectionLeft();
                transform.position = new Vector3(transform.position.x - (3 * Time.deltaTime), transform.position.y, transform.position.z);
                MoveAnimation();
            }

        }

        else
        {
            IdleAnimation();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {           
            AttackAnimation();
        }
    }

    void FlipDirectionRight()
    {
        if(!PlayerSprite.flipX)
        PlayerSprite.flipX = true;
    }

    void FlipDirectionLeft()
    {
        if (PlayerSprite.flipX)
            PlayerSprite.flipX = false;
    }

    void IdleAnimation()
    {
        CurrentAnimation.SetInteger("AnimationPlayer", 0);
    }

    void MoveAnimation()
    {
        CurrentAnimation.SetInteger("AnimationPlayer", 1);
    }

    void AttackAnimation()
    {
        CurrentAnimation.SetInteger("AnimationPlayer", 2);
    }


}
