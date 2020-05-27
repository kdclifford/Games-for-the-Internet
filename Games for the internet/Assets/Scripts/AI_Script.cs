using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using AiFunctions.Utils;
using UnityEngine;

public class AI_Script : MonoBehaviour
{
    public float RayDistanceForward;
    public float RayDistanceDown;
    public List<string> enemyTags;
    public GameObject player;



    //private List<CNode> path = new List<CNode>();

    // public float radius;


    private bool lastSeen = false;


    // Start is called before the first frame update


    private int AgentGridPosX, AgentGridPosY;
    private int PlayerGridPosX, PlayerGridPosY;
    private int PlayerLastGridPosX, PlayerLastGridPosY;


    //Vector2 agentNewHeight = new Vector2(agent.GetComponent<CapsuleCollider2D>().bounds.center.x, agent.GetComponent<CapsuleCollider2D>().bounds.center.y - agent.GetComponent<CapsuleCollider2D>().bounds.extents.y);
   // KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out PlayerGridPosX, out PlayerGridPosY);

    // Update is called once per frame
    //public bool FindPath(GameObject insectObj, Grid terrainMap, float sightRange, ref List<CNode> path)
    //{
    //    Collider2D AgentCollider = insectObj.GetComponent<CapsuleCollider2D>();
    //    Rigidbody2D Body = insectObj.GetComponent<Rigidbody2D>();

    //    // bool isGrounded = KylesFunctions.isGrounded2D(AgentCollider, RayDistanceDown, floorMask);
    //    // bool isNextToWall = KylesFunctions.IsNextToWall2D(transform.localScale.x, AgentCollider, RayDistanceForward, floorMask);


    //    if (AiMaths.SightSphere(AgentCollider, sightRange))
    //    {
    //        Vector3 playerLastSeenPos;


    //        KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out PlayerGridPosX, out PlayerGridPosY);
    //        KylesFunctions.AStar(terrainMap, AgentGridPosX, AgentGridPosY, PlayerLastGridPosX, PlayerLastGridPosY, 2, false, ref path);
    //      //  KylesFunctions.GetXY(transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out AgentGridPosX, out AgentGridPosY);

    //        if (PlayerGridPosX != PlayerLastGridPosX | PlayerGridPosY != PlayerLastGridPosY)
    //        {
    //            playerLastSeenPos = player.transform.position;

    //            if (path.Count >= 2)
    //            {
    //                path.RemoveAt(path.Count - 1);
    //            }
    //            if (path.Count >= 1)
    //            {
    //                path.RemoveAt(0);
    //            }

    //        }

           

    //        if (path.Count != 0)
    //        {
    //            //for (int i = 0; i < path.Count - 1; i++)
    //            //{
    //            //    terrainMap.SetValue(path[i].x, path[i].y, terrainMap.GetValue(path[i].x, path[i].y), Color.blue);
    //            //}

    //            Vector3 firstStep = terrainMap.GetWorldPosition(path[path.Count - 1].x, path[path.Count - 1].y);
    //            firstStep.x += 0.5f;
    //            firstStep.y += 0.5f;

    //            Vector3 agentNewHieght = new Vector3(AgentCollider.bounds.center.x, (AgentCollider.bounds.center.y - AgentCollider.bounds.extents.y) + 0.5f, 0);

    //            float dotProb = AgentCollider.bounds.center.x - firstStep.x;
    //            float distanceFromNode = (new Vector3(AgentCollider.bounds.center.x, (AgentCollider.bounds.center.y - AgentCollider.bounds.extents.y) + 0.5f, 0) - firstStep).magnitude;





    //        }
           










    //        return true;
    //    }
    //    else
    //    {
    //        // Body.velocity = new Vector2(0, Body.velocity.y);
    //        //AgentVelocity = new Vector2(0, Body.velocity.y);
    //    }



    //    return false;


    //}










    private void FixedUpdate()
    {
        // Body.velocity = Vector3.SmoothDamp(Body.velocity, AgentVelocity, ref m_Velocity, 0.5f);
        //Body.AddForce(new Vector2(0, jumpForce.y));
        //jumpForce = Vector2.zero;
    }









}
