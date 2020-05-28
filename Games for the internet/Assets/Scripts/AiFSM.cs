using System.Collections;
using System.Collections.Generic;
using AiFunctions.Utils;
using Functions.Utils;
using AiAnimationController.Utils;
using UnityEngine.Tilemaps;
using UnityEngine;

public class AiFSM : MonoBehaviour
{
    public LayerMask playerMask;
    public LayerMask floorMask;
    public Animator agentAnimator;
    public AiStates currentState = AiStates.Chase;
    private Grid terrainMap;
    private AiAgentInfo agentInfo;
    private Collider2D agentCollider;
    public GameObject player;
    private List<CNode> path = new List<CNode>();
    bool getTerrain = false;
    public bool showSight = true;

    private int agentx;
    private int agenty;

    Vector2Int playerGridPos;
    Vector2Int newPlayerPos;

    bool getPlayerPos = false;
    bool findPath = true;
    Vector2Int agentGridPos;

    private void Start()
    {
        //terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();
        agentInfo = GetComponent<AiAgentInfo>();
        agentCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(agentCollider.bounds.center, Vector2.right, Color.red);
        //Get TerrainMap
        if (!getTerrain)
        {
            terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();
            getTerrain = true;
        }

        if (currentState == AiStates.Patrol)
        {
            AiAnimations.Walk(agentAnimator);
        }
        else if (currentState == AiStates.Chase)
        {
            if (KylesFunctions.IsNextToWall2D(transform.localScale.x, agentCollider, 0.5f, playerMask))
            {
                path.Clear();
                currentState = AiStates.Attack;
            }
            else
            {

                AiAnimations.Walk(agentAnimator);



                if (AiMaths.SightSphere(agentCollider, agentInfo.sightRange, playerMask))
                {

                        Vector2 agentNewHeight = new Vector2(GetComponent<CapsuleCollider2D>().bounds.center.x, GetComponent<CapsuleCollider2D>().bounds.center.y - GetComponent<CapsuleCollider2D>().bounds.extents.y);
                    if (!getPlayerPos)
                    {
                        // KylesFunctions.GetXY(agentNewHeight, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out playerGridPos);
                        KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out playerGridPos);
                        getPlayerPos = true;                        
                    }

                        KylesFunctions.GetXY(agentNewHeight, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out agentGridPos);

                    KylesFunctions.GetXY(player.transform.position, terrainMap.GetOriginPos(), terrainMap.GetCellSize(), out newPlayerPos);


                    if (newPlayerPos != playerGridPos)
                    {
                        // playerGridPos = newPlayerPos;
                        getPlayerPos = false;
                        path.Clear();
                        findPath = true;
                    }


                    if (findPath)
                    {
                        KylesFunctions.AStar(terrainMap, agentGridPos.x, agentGridPos.y, playerGridPos.x, playerGridPos.y, 2, false, ref path);
                        findPath = false;
                    }
                }

                

            }
        }
        else if (currentState == AiStates.Attack)
        {
            AiAnimations.Attack(agentAnimator);

            if(!AiMaths.SightSphere(agentCollider, 1.0f, playerMask))
            {
                currentState = AiStates.Chase;
            }


        }



    }

    private void FixedUpdate()
    {
        if (path.Count >= 1)
        {

            AiMaths.MoveAgentTo(gameObject, agentGridPos, agentInfo.speed, agentInfo.jumpHeight, new Vector2Int(path[path.Count - 1].x, path[path.Count - 1].y), floorMask, ref path);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && showSight)
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



   
}