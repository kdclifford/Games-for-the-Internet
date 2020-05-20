using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;

public class AI_Script : MonoBehaviour
{
    
    public LayerMask floorMask;
    public LayerMask playerMask;
    public float RayDistanceForward;
    public float RayDistanceDown;
    public float SightRange;
    public List<string> enemyTags;
    private float MaxSpeed = 10.0f;
    public GameObject player;
    public float jumpHeight;

   

    private List<CNode> path = new List<CNode>();
    private bool getTerrain = false;

    public int test = 1;
    public float radius;

    private bool oneTime = false;
    private bool lastSeen = false;
    [SerializeField]
    private int gridCount = 1;

    // Start is called before the first frame update


    private int AgentGridPosX, AgentGridPosY;
    private int PlayerGridPosX, PlayerGridPosY;
    private int PlayerLastGridPosX, PlayerLastGridPosY;

    // Update is called once per frame
  public bool ChasePlayer(GameObject insectObj, Grid terrainMap )
    {
        Collider2D AgentCollider = insectObj.GetComponent<CapsuleCollider2D>();
        Rigidbody2D Body = insectObj.GetComponent<Rigidbody2D>();

        bool isGrounded = KylesFunctions.isGrounded2D(AgentCollider, RayDistanceDown, floorMask);
           // bool isNextToWall = KylesFunctions.IsNextToWall2D(transform.localScale.x, AgentCollider, RayDistanceForward, floorMask);
      

        if (SightSphere(AgentCollider))
        {
            Vector3 playerLastSeenPos;






            KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out PlayerGridPosX, out PlayerGridPosY);
            KylesFunctions.GetXY(transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out AgentGridPosX, out AgentGridPosY);

            if (PlayerGridPosX != PlayerLastGridPosX | PlayerGridPosY != PlayerLastGridPosY)
            {

                //Is Player left or Right
                //float dotProb = transform.position.x - playerLastSeenPos.x;

                // float test = terrainMap.GetValue(transform.position);
                lastSeen = false;
                gridCount = 1;
                path.Clear();

            }

            if (!lastSeen)
            {
                playerLastSeenPos = player.transform.position;
                lastSeen = true;
                KylesFunctions.GetXY(playerLastSeenPos, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out PlayerLastGridPosX, out PlayerLastGridPosY);
                KylesFunctions.AStar(terrainMap, AgentGridPosX, AgentGridPosY, PlayerLastGridPosX, PlayerLastGridPosY, 2, false, ref path);

                if (path.Count >= 2)
                {
                    path.RemoveAt(path.Count - 1);
                }
                if (path.Count >= 1)
                {
                    path.RemoveAt(0);
                }
            }

            if (path.Count != 0)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    terrainMap.SetValue(path[i].x, path[i].y, terrainMap.GetValue(path[i].x, path[i].y), Color.blue);
                }

                Vector3 firstStep = terrainMap.GetWorldPosition(path[path.Count - 1].x, path[path.Count - 1].y);
                firstStep.x += 0.5f;
                firstStep.y += 0.5f;

                Vector3 agentNewHieght = new Vector3(AgentCollider.bounds.center.x, (AgentCollider.bounds.center.y - AgentCollider.bounds.extents.y) + 0.5f, 0);

                float dotProb = AgentCollider.bounds.center.x - firstStep.x;
                float distanceFromNode = (new Vector3(AgentCollider.bounds.center.x, (AgentCollider.bounds.center.y - AgentCollider.bounds.extents.y) + 0.5f, 0) - firstStep).magnitude;
                float yVelocity = Body.velocity.y;

                if (distanceFromNode >= radius && dotProb < 0)
                {


                    Vector3 AgentScale = transform.localScale;
                    AgentScale.x = 1;
                    transform.localScale = AgentScale;


                    if (firstStep.y > agentNewHieght.y + 0.1f)
                    {
                        if (isGrounded)
                        {
                            Body.velocity = Vector2.up * jumpHeight;
                            //jumpForce.y += jumpHeight;
                            //jump = false;
                        }
                        //else if (isGrounded && !jump)
                        //{
                        //    // jumpForce.y = 0;
                        //    jump = true;
                        //}
                    }
                    else
                    {
                        AgentVelocity = new Vector2(MaxSpeed * distanceFromNode, 0);
                    }



                }
                else if (distanceFromNode >= radius && dotProb > 0)
                {
                    Vector3 AgentScale = transform.localScale;
                    AgentScale.x = -1;
                    transform.localScale = AgentScale;

                    if (firstStep.y > agentNewHieght.y + 0.1f)
                    {
                        if (isGrounded && jump)
                        {
                            Body.velocity = Vector2.up * jumpHeight;
                            //jumpForce.y += jumpHeight;
                            jump = false;
                        }
                        else if (isGrounded && !jump)
                        {
                            // jumpForce.y = 0;
                            jump = true;
                        }
                    }
                    else
                    {
                    AgentVelocity = new Vector2(-MaxSpeed * distanceFromNode, 0);
                    }


                }
                else
                {
                    //Body.velocity = new Vector2(0, Body.velocity.y);
                    //AgentVelocity = new Vector2(0, Body.velocity.y);

                    path.Remove(path[path.Count - 1]);

                    //if (gridCount + 1 < path.Count)
                    //{
                    //    gridCount++;
                    //}

                }
            }










            return true;
        }
        else
        {
            Body.velocity = new Vector2(0, Body.velocity.y);
            AgentVelocity = new Vector2(0, Body.velocity.y);
        }



        return false;


    }

  





    public bool SightSphere(Collider2D agentColloder)
    {
        if (Application.isPlaying)
        {
            Collider2D hitColl = Physics2D.OverlapCircle(agentColloder.bounds.center, SightRange, playerMask);

            if (hitColl != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }


    }


    private void FixedUpdate()
    {
       // Body.velocity = Vector3.SmoothDamp(Body.velocity, AgentVelocity, ref m_Velocity, 0.5f);
        //Body.AddForce(new Vector2(0, jumpForce.y));
        //jumpForce = Vector2.zero;
    }









}
