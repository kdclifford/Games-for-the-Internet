﻿using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;

public class AI_Script : MonoBehaviour
{
    private CapsuleCollider2D AgentCollider = null;
    public LayerMask floorMask;
    public LayerMask playerMask;
    public float RayDistanceForward;
    public float RayDistanceDown;
    public float SightRange;
    public List<string> enemyTags;
    private Vector2 AgentVelocity = new Vector2();
    private Rigidbody2D Body;
    private float MaxSpeed = 10.0f;
    private Vector3 m_Velocity = Vector3.zero;
    private Vector2 jumpForce = new Vector2();
    public GameObject player;
    private bool jump = false;
    public float jumpHeight;

    private Grid terrainMap;

    private bool getTerrain = false;

    private bool oneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        AgentCollider = gameObject.GetComponent<CapsuleCollider2D>();
        Body = gameObject.GetComponent<Rigidbody2D>();
        //terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();
    }

    // Update is called once per frame
    void Update()
    {
        //Get TerrainMap
        if(!getTerrain)
        {
            terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();
            getTerrain = false;
        }

        if (SightSphere())
        {
            bool isGrounded = KylesFunctions.isGrounded2D(AgentCollider, RayDistanceDown, floorMask);
            bool isNextToWall = KylesFunctions.IsNextToWall2D(transform.localScale.x, AgentCollider, RayDistanceForward, floorMask);
            Vector2 playerLastSeenPos = player.transform.position;

            //Is Player left or Right
            float dotProb = transform.position.x - playerLastSeenPos.x;

            // float test = terrainMap.GetValue(transform.position);
            int AgentGridPosX, AgentGridPosY;
            int PlayerGridPosX, PlayerGridPosY;


            KylesFunctions.GetXY(transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out AgentGridPosX, out AgentGridPosY);
            KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out PlayerGridPosX, out PlayerGridPosY);

            if (oneTime == false)
            {
                KylesFunctions.AStar(terrainMap, AgentGridPosX, AgentGridPosY, PlayerGridPosX, PlayerGridPosY, 2, false);
                oneTime = true;
            }

            //terrainMap.SetValue(terrainMap.GetWorldPosition(AgentGridPosX, AgentGridPosY), 5);
            //terrainMap.SetValue(terrainMap.GetWorldPosition(PlayerGridPosX, PlayerGridPosY), 7);
            Debug.Log(AgentGridPosX + "  " + AgentGridPosY + " oioi" );
            Debug.Log(terrainMap.GetValue(3, 1));








            //if(dotProb > 0 && isNextToWall ==false)
            //{
            //    AgentVelocity = new Vector2(-MaxSpeed, Body.velocity.y);
            //    Vector3 AgentScale = transform.localScale;
            //    AgentScale.x = -1;
            //    transform.localScale = AgentScale;
            //}
            //else if(dotProb < 0 && isNextToWall == false)
            //{
            //    AgentVelocity = new Vector2(MaxSpeed, Body.velocity.y);
            //    Vector3 AgentScale = transform.localScale;
            //    AgentScale.x = 1;
            //    transform.localScale = AgentScale;
            //}
            //else
            //{
            //    Body.velocity = new Vector2(0, Body.velocity.y);
            //    AgentVelocity = new Vector2(0, Body.velocity.y);
            //}

            //Debug.Log(dotProb);

            //if (isNextToWall && isGrounded && jump)
            //{

            //    jumpForce.y += jumpHeight;
            //    jump = false;
            //}
            //else if (isGrounded && !jump)
            //{
            //    jump = true;
            //}

        }
        else
        {
            Body.velocity = new Vector2(0, Body.velocity.y);
            AgentVelocity = new Vector2(0, Body.velocity.y);
        }

       




    }

    private void OnDrawGizmos()
    {
        if (SightSphere())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, SightRange);
    }



    //private bool isGrounded()
    //{

    //    RaycastHit2D hit = Physics2D.BoxCast(AgentCollider.bounds.center, AgentCollider.bounds.size, 0f, Vector2.down, RayDistanceDown, floorMask);
    //    Color rayColour;
    //    if (hit.collider != null)
    //    {
    //        rayColour = Color.green;
    //    }
    //    else
    //    {
    //        rayColour = Color.red;
    //    }
    //    Debug.DrawRay(AgentCollider.bounds.center + new Vector3(AgentCollider.bounds.extents.x, 0), Vector2.down * (AgentCollider.bounds.extents.y + RayDistanceDown), rayColour);
    //    Debug.DrawRay(AgentCollider.bounds.center - new Vector3(AgentCollider.bounds.extents.x, 0), Vector2.down * (AgentCollider.bounds.extents.y + RayDistanceDown), rayColour);
    //    Debug.DrawRay(AgentCollider.bounds.center - new Vector3(AgentCollider.bounds.extents.x, AgentCollider.bounds.extents.y + RayDistanceDown), Vector3.right * (AgentCollider.bounds.extents.x * 2), rayColour);
    //    Debug.Log(hit.collider);

    //    return hit.collider != null;

    //}

    //private bool IsNextToWall()
    //{
    //    RaycastHit2D hit;
    //    if (transform.localScale.x == 1)
    //    {
    //        hit = Physics2D.BoxCast(AgentCollider.bounds.center, AgentCollider.bounds.size, 0f, Vector2.right, RayDistanceForward, floorMask);
    //    }
    //    else
    //    {
    //        hit = Physics2D.BoxCast(AgentCollider.bounds.center, AgentCollider.bounds.size, 0f, Vector2.left, RayDistanceForward, floorMask);

    //        //  hit = Physics2D.Raycast(AgentCollider.bounds.center, Vector2.left, AgentCollider.bounds.extents.x + RayDistance, floorMask);
    //    }


    //    Color rayColour;
    //    if (hit.collider != null)
    //    {
    //        rayColour = Color.green;
    //    }
    //    else
    //    {
    //        rayColour = Color.red;
    //    }

    //    if (transform.localScale.x == 1)
    //    {
    //        Debug.DrawRay(AgentCollider.bounds.center + new Vector3(AgentCollider.bounds.extents.x, 0), Vector3.right * RayDistanceForward, rayColour);
    //    }
    //    else
    //    {
    //        Debug.DrawRay(AgentCollider.bounds.center - new Vector3(AgentCollider.bounds.extents.x, 0), Vector3.left * RayDistanceForward, rayColour);

    //    }

    //    Debug.Log(hit.collider);

    //    return hit.collider != null;

    //}

    private bool SightSphere()
    {
        if (Application.isPlaying)
        {
            Collider2D hitColl = Physics2D.OverlapCircle(AgentCollider.bounds.center, SightRange, playerMask);

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
        Body.velocity = Vector3.SmoothDamp(Body.velocity, AgentVelocity, ref m_Velocity, 0.5f);
        Body.AddForce(new Vector2(jumpForce.x, jumpForce.y));
        jumpForce = Vector2.zero;
    }









}
