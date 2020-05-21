using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AiFunctions.Utils
{

  public class AiMaths
    {
        public static bool SightSphere(Collider2D agentCollider, float sightRange)
        {
            if (Application.isPlaying)
            {
                Collider2D hitColl = Physics2D.OverlapCircle(agentCollider.bounds.center, sightRange, 9);

                if (hitColl != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }            
                return false;
        }


        //Ai Move
//        if (distanceFromNode >= radius && dotProb< 0)
//                {


//                    Vector3 AgentScale = transform.localScale;
//        AgentScale.x = 1;
//                    transform.localScale = AgentScale;


//                    if (firstStep.y > agentNewHieght.y + 0.1f)
//                    {
//                        if (isGrounded)
//                        {
//                            Body.velocity = Vector2.up* jumpHeight;
//    }

//}
//                    else
//                    {
//                        AgentVelocity = new Vector2(MaxSpeed* distanceFromNode, 0);
//                    }



//                }
//                else if (distanceFromNode >= radius && dotProb > 0)
//                {
//                    Vector3 AgentScale = transform.localScale;
//AgentScale.x = -1;
//                    transform.localScale = AgentScale;

//                    if (firstStep.y > agentNewHieght.y + 0.1f)
//                    {
//                        if (isGrounded)
//                        {
//                            Body.velocity = Vector2.up* jumpHeight;
                         
//                        }
                      
//                    }
//                    else
//                    {
//                    AgentVelocity = new Vector2(-MaxSpeed* distanceFromNode, 0);
//                    }



    }
}
