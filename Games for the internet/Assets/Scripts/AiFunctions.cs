using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AiFunctions.Utils
{

  public class AiMaths
    {
        public static bool SightSphere(Collider2D agentCollider, float sightRange, LayerMask mask)
        {

            Collider2D hitColl = Physics2D.OverlapCircle(agentCollider.bounds.center, sightRange, mask);

            if (hitColl != null)
            {
                return true;
            }
            else
            {
                return false;
            }

            //return false;
        }


        public static void MoveAgentTo(GameObject agent, Vector2 agentGridPos, float speed, Vector2 goalPos, ref List<CNode> path)
        {
           Rigidbody2D agentRig = agent.GetComponent<Rigidbody2D>();
            Vector2 agentPos = agent.transform.position;

           

            float dotProd = agentGridPos.x - goalPos.x;

            float directionalSpeed = 0;

            if (Mathf.Abs( dotProd) > 0.5f)
            {

                if (dotProd < 0)
                {
                    directionalSpeed = speed;
                }
                else if (dotProd > 0)
                {
                    directionalSpeed = -speed;
                }
            }
            else
            {
                agent.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 0));
                path.RemoveAt(0);
            }

            //get grid x and y to make sure it gets to the correct position
            //while()

            agent.GetComponent<Rigidbody2D>().AddForce(new Vector2(directionalSpeed, 0));



        }

        void playerPositionChanged()
        {

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
