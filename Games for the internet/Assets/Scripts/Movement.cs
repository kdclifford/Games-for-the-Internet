using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using Functions.Utils;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private SpriteRenderer PlayerSprite;
    public Animator CurrentAnimation;
    private Rigidbody2D Body;
    private CapsuleCollider2D normalCollider;
    private CapsuleCollider2D crouchCollider;
    public float jumpAmount;

    [SerializeField]
    private List<LayerMask> floorMask;
    public float hoverAmount;
    private Vector3 m_Velocity = Vector3.zero;

    private float MaxSpeed = 10.0f;
    private Vector2 playerVelocity = new Vector2();
    private Vector2 jumpForce = new Vector2();

    private bool FlipX = false;
    private bool isAttacking = false;
    public bool isHurt = false;

    public GameObject attackBox;

    public GameObject dropCollider;
    public GameObject slamCollider;

    //Power Up Bools
    public bool wings = false;
    public bool shoot = false;
    public bool block = false;

    public bool jumpAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSprite = gameObject.GetComponent<SpriteRenderer>();
        Body = gameObject.GetComponent<Rigidbody2D>();
        normalCollider = gameObject.GetComponent<CapsuleCollider2D>();
        crouchCollider = dropCollider.GetComponent<CapsuleCollider2D>();
        FlipX = false;
    }

    bool jump = false;
    bool crouch = false;
   
    // Update is called once per frame
    void Update()
    {


        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        else if (KylesFunctions.isGrounded2D(normalCollider, 0.1f, floorMask) | KylesFunctions.isGrounded2D(crouchCollider, 0.1f, floorMask))
        {
            jump = false;
        }

        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }

        if (jump && crouch)
        {
            slamCollider.SetActive(true);
        }
        else
        {
            slamCollider.SetActive(false);
        }

        if (KylesFunctions.isGrounded2D(normalCollider, 0.1f, floorMask) && !isAttacking && Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
            StartCoroutine(DoAttack());
            jumpAnimation = false;

            Body.velocity = new Vector2(0, Body.velocity.y);
            playerVelocity = new Vector2(0, Body.velocity.y);
        }
        else
        {
            IdleAnimation();
        }

        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.W))
            {
                if (!CurrentAnimation.GetCurrentAnimatorStateInfo(0).IsName("Jump") && KylesFunctions.isGrounded2D(normalCollider, 0.01f, floorMask) && raycast(normalCollider, floorMask, 0.01f))
                {
                    
                    JumpAnimation();
                    jumpAnimation = true;
                    
                    //playerVelocity = new Vector2(Body.velocity.x, 10);
                    jumpForce.y += (jumpAmount * 100.0f) * 2;
                }
            }

            else if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
            {
                playerVelocity = new Vector2(0, -MaxSpeed);
                dropCollider.SetActive(true);
                GetComponent<CapsuleCollider2D>().enabled = false;
                //GetComponent<BoxCollider2D>().enabled = false;
                DropAnimation();
            }
            else
            {
                GetComponent<CapsuleCollider2D>().enabled = true;
               // GetComponent<BoxCollider2D>().enabled = true;
                dropCollider.SetActive(false);
            }

            if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
            {
                playerVelocity = new Vector2(MaxSpeed, Body.velocity.y);
                FlipDirectionRight();

                if (!crouch)
                {
                    if (!jumpAnimation)
                    {
                        MoveAnimation();
                    }
                }
            }


            else if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
            {
                playerVelocity = new Vector2(-MaxSpeed, Body.velocity.y);
                FlipDirectionLeft();
                if (!crouch)
                {
                    if (!jumpAnimation)
                    {
                        MoveAnimation();
                    }
                }
            }
            else
            {
               
                if (!crouch)
                {
                    if (!jumpAnimation)
                    {
                        IdleAnimation();
                    }
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


        }

       

        if (isHurt)
        {
            StartCoroutine(DoDamage());
        }






    }

    IEnumerator DoAttack()
    {
        
        AttackAnimation();
        attackBox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        //yield return new WaitForSeconds(0.5f);
        attackBox.SetActive(false);
        isAttacking = false;
    }

    IEnumerator DoDamage()
    {
        HitAnimation();
        
        yield return new WaitForSeconds(0.3f);

        isHurt = false;
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            Body.velocity = Vector3.SmoothDamp(Body.velocity, playerVelocity, ref m_Velocity, 0.5f);
            Body.AddForce(new Vector2(jumpForce.x, jumpForce.y));
            jumpForce = Vector2.zero;
            // Body.velocity = Vector3.SmoothDamp(Body.velocity, new Vector2(Body.velocity.x - (isMovingLeft * 10), Body.velocity.y), ref m_Velocity, 0.5f);
            // Body.velocity = (new Vector2(Body.velocity.x - (isMovingLeft ), Body.velocity.y));;
            // Body.MovePosition( new Vector3(transform.position.x, transform.position.y + (isJumping)));
        }
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

    void DropAnimation()
    {
        CurrentAnimation.SetInteger("AnimationPlayer", 4);
    }
   public void HitAnimation()
    {
        CurrentAnimation.SetInteger("AnimationPlayer", 5);
    }

    public void DieAnimation()
    {
        CurrentAnimation.SetInteger("AnimationPlayer", 6);
    }


    public void IsHit()
    {
        isHurt = true;
        //HitAnimation();


    }

    public void IsNotHit()
    {
        isHurt = false;
    }

    void jumpAnimationOff()
    {
        jumpAnimation = false;
    }

    bool raycast(Collider2D col, List<LayerMask> mask, float distance)
    {
        RaycastHit2D hit;
        Color colour;
        foreach (LayerMask node in mask)
        {
            // hit = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.1f, node);
            hit = Physics2D.BoxCast(col.bounds.center, new Vector3(col.bounds.size.x - 0.01f, col.bounds.extents.y ,0), 0, Vector2.down, col.bounds.extents.y + 0.1f, node);


            Debug.DrawRay(col.bounds.center, Vector2.down * (col.bounds.extents.y + 0.1f), Color.red);


            if(hit)
            {
                return true;
            }
        }
        return false;
    }

   

}
