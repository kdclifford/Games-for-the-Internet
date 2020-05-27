using System.Collections;
using System.Collections.Generic;
using AiFunctions.Utils;
using Functions.Utils;
using UnityEngine.Tilemaps;
using UnityEngine;

public class AiFSM : MonoBehaviour
{
    public LayerMask playerMask;
    public AiStates currentState = AiStates.Chase;
    private Grid terrainMap;
    private AiAgentInfo agentInfo;
    private Collider2D agentCollider;
    public GameObject player;
    private List<CNode> path = new List<CNode>();
    bool getTerrain = false;

    private int agentx;
    private int agenty;

    Vector2Int playerGridPos;
    Vector2Int newPlayerPos;

    private void Start()
    {
        //terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();
        agentInfo = GetComponent<AiAgentInfo>();
        agentCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get TerrainMap
        if (!getTerrain)
        {
            terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();
            getTerrain = true;
        }

        if (currentState == AiStates.Patrol)
        {

        }
        else if (currentState == AiStates.Chase)
        {


            Vector2Int agentGridPos;

           
            Vector2 agentNewHeight = new Vector2(GetComponent<CapsuleCollider2D>().bounds.center.x, GetComponent<CapsuleCollider2D>().bounds.center.y - GetComponent<CapsuleCollider2D>().bounds.extents.y);
            KylesFunctions.GetXY(agentNewHeight, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out agentGridPos);
           // KylesFunctions.GetXY(agentNewHeight, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out playerGridPos);


            if (AiMaths.SightSphere(agentCollider, agentInfo.sightRange, playerMask))
            {

                KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out playerGridPos);

                
                KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out newPlayerPos);


                if (newPlayerPos != playerGridPos)
                {
                    path.Clear();
                }
                path.Clear();

                if (path.Count == 0)
                {
                     KylesFunctions.AStar(terrainMap, agentGridPos.x, agentGridPos.y, playerGridPos.x, playerGridPos.y, 2, false, ref path);
                }
            }

            if (path.Count >= 1)
            {

                AiMaths.MoveAgentTo(gameObject, agentGridPos, agentInfo.speed, new Vector2Int( path[0].x, path[0].y), ref path);
            }
        }
        else if (currentState == AiStates.Attack)
        {

        }



    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (AiMaths.SightSphere(agentCollider, agentInfo.sightRange, playerMask))
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawWireSphere(transform.position, agentInfo.sightRange);
        }
    }



    //public bool SightSphere(Collider2D agentCollider, float sightRange, LayerMask mask)
    //{

    //    Collider2D hitColl = Physics2D.OverlapCircle(agentCollider.bounds.center, sightRange, mask);

    //    if (hitColl != null)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //    //return false;
    //}
}