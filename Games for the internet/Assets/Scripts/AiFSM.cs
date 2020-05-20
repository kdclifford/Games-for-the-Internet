using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFSM : MonoBehaviour
{
    public AiStates currentState = AiStates.Patrol;
    private Grid terrainMap;

    private void Start()
    { 
            terrainMap = GameObject.FindGameObjectWithTag("TerrainMap").GetComponent<GridSettings>().GetGrid();        
    }

// Update is called once per frame
void Update()
    {
        if(currentState == AiStates.Patrol)
        {
AI_Script.ch
        }
        else if (currentState == AiStates.Chase)
        {

        }
        else if (currentState == AiStates.Attack)
        {

        }



    }


    //private void OnDrawGizmos()
    //{
    //    if (SightSphere())
    //    {
    //        Gizmos.color = Color.green;
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.red;
    //    }
    //    Gizmos.DrawWireSphere(transform.position, SightRange);
    //}
}
