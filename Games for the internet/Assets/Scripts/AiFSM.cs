using System.Collections;
using System.Collections.Generic;
using AiFunctions.Utils;
using UnityEngine;

public class AiFSM : MonoBehaviour
{
    public AiStates currentState = AiStates.Patrol;
    private Grid terrainMap;
    private AiAgentInfo agentInfo;
    private Collider2D agentCollider;

    private void Start()
    { 
            terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();
        agentInfo = GetComponent<AiAgentInfo>();
        agentCollider = GetComponent<Collider2D>();
    }

// Update is called once per frame
void Update()
    {
        if(currentState == AiStates.Patrol)
        {
            
        }
        else if (currentState == AiStates.Chase)
        {

        }
        else if (currentState == AiStates.Attack)
        {

        }



    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (AiMaths.SightSphere(agentCollider, agentInfo.sightRange))
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
