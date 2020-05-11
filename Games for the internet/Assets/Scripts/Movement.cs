﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private SpriteRenderer PlayerSprite;
    public Animator CurrentAnimation;
    private Rigidbody2D Body;
    private CapsuleCollider2D playerCollider;
    public float jumpAmount;

    [SerializeField]
    private LayerMask floorMask;
    public float hoverAmount;
    private Vector3 m_Velocity = Vector3.zero;

    private float MaxSpeed = 10.0f;
    private Vector2 playerVelocity = new Vector2();
    private Vector2 jumpForce = new Vector2();

    private bool FlipX = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSprite = gameObject.GetComponent<SpriteRenderer>();
        Body = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        FlipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CurrentAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
           if (isGrounded() && Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.W))
            {
                if (!CurrentAnimation.GetCurrentAnimatorStateInfo(0).IsName("Jump") | isGrounded())
                {
                    JumpAnimation();
                    //playerVelocity = new Vector2(Body.velocity.x, 10);
                    jumpForce.y += (jumpAmount * 100.0f) * 2;
                }
            }

            else if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
            {
                playerVelocity = new Vector2(MaxSpeed, Body.velocity.y);
                FlipDirectionRight();
                MoveAnimation();
            }

            else if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
            {
                playerVelocity = new Vector2(-MaxSpeed, Body.velocity.y);
                FlipDirectionLeft();
                MoveAnimation();
            }


            else
            {
                IdleAnimation();
                Body.velocity = new Vector2(0, Body.velocity.y);
                playerVelocity = new Vector2(0, Body.velocity.y);
                //isJumping = 0;
            }
        }


        //else
        //{
        //    IdleAnimation();
        //    Body.velocity = new Vector2(0, Body.velocity.y);
        //    playerVelocity = new Vector2(0, Body.velocity.y);
        //    // isJumping = 0;
        //}

        if (Input.GetKeyDown(KeyCode.J))
        {           
            AttackAnimation();
        }


    }

    void FixedUpdate()
    {
        //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        Body.velocity = Vector3.SmoothDamp(Body.velocity, playerVelocity, ref m_Velocity, 0.5f);
        Body.AddForce( new Vector2( jumpForce.x, jumpForce.y));
        jumpForce = Vector2.zero;
        // Body.velocity = Vector3.SmoothDamp(Body.velocity, new Vector2(Body.velocity.x - (isMovingLeft * 10), Body.velocity.y), ref m_Velocity, 0.5f);
        // Body.velocity = (new Vector2(Body.velocity.x - (isMovingLeft ), Body.velocity.y));;
        // Body.MovePosition( new Vector3(transform.position.x, transform.position.y + (isJumping)));

    }

    private bool isGrounded()
    {
        float extraHieght = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraHieght, floorMask);
        Color rayColour;
        if(hit.collider != null)
        {
            rayColour = Color.green;
        }
       else
        {
            rayColour = Color.red;
        }
        Debug.DrawRay(playerCollider.bounds.center + new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraHieght), rayColour);
        Debug.DrawRay(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraHieght), rayColour);
        Debug.DrawRay(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x, playerCollider.bounds.extents.y + extraHieght), Vector3.right * (playerCollider.bounds.extents.x * 2), rayColour);
       // Debug.Log(hit.collider);

        return hit.collider != null;

    }

    void FlipDirectionRight()
    {
        if (FlipX)
        {
            FlipX = false;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    void FlipDirectionLeft()
    {
        if (!FlipX)
        {
            FlipX = true;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
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

    void JumpAnimation()
    {
        CurrentAnimation.SetInteger("AnimationPlayer", 3);
    }
}
