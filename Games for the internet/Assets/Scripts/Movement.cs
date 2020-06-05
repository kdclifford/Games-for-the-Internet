using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
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

    public GameObject attackBox;

    public GameObject dropCollider;
    public GameObject slamCollider;

    //Power Up Bools
    public bool wings = false;
    public bool shoot = false;

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
        else if(isGrounded(normalCollider) | isGrounded(crouchCollider))
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

        if(jump && crouch)
        {
            slamCollider.SetActive(true);
        }
        else
        {
            slamCollider.SetActive(false);
        }


            if (isGrounded(normalCollider) && !isAttacking && Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
            
            StartCoroutine(DoAttack());

            Body.velocity = new Vector2(0, Body.velocity.y);
            playerVelocity = new Vector2(0, Body.velocity.y);


        }




        if (!isAttacking )
        {
           




          

              if (isGrounded(normalCollider) && Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.W))
            {
                if (!CurrentAnimation.GetCurrentAnimatorStateInfo(0).IsName("Jump") | isGrounded(normalCollider))
                {
                    JumpAnimation();
                    //playerVelocity = new Vector2(Body.velocity.x, 10);
                    jumpForce.y += (jumpAmount * 100.0f) * 2;
                }
            }

              

            else if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
            {
                playerVelocity = new Vector2(0, -MaxSpeed);
                dropCollider.SetActive(true);
                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                DropAnimation();
            }
              else
            {
                GetComponent<CapsuleCollider2D>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                dropCollider.SetActive(false);
            }

            if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
            {
                playerVelocity = new Vector2(MaxSpeed, Body.velocity.y);
                FlipDirectionRight();

                if (!crouch)
                {
                    MoveAnimation();
                }
            }
            

            else if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
            {
                playerVelocity = new Vector2(-MaxSpeed, Body.velocity.y);
                FlipDirectionLeft();
                if (!crouch)
                {
                    MoveAnimation();
                }
            }
            else
            {
                if (!crouch)
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

    private bool isGrounded(Collider2D playerCollider )
    {
        float extraHieght = 0.1f;
        RaycastHit2D hit;
        foreach (LayerMask node in floorMask)
        {

            hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraHieght, node);
            if (hit)
            {
                Color rayColour;
                if (hit.collider != null)
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
        }

        return false;

        

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
}
