using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
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


        public static void MoveAgentTo(GameObject agent, Vector2Int agentGridPos, float speed, float jumpHeight, Vector2Int goalPos, List<LayerMask> floor, ref List<CNode> path)
        { 
           Rigidbody2D agentRig = agent.GetComponent<Rigidbody2D>();
            //Vector2Int agentPos = KylesFunctions.GetXY()

            bool isOnGrounded = KylesFunctions.isGrounded2D(agent.GetComponent<Collider2D>(), 0.1f, floor, agent);

           // agent.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 0));

            float dotProd = agentGridPos.x - goalPos.x;
            float distance = Vector2Int.Distance(agentGridPos, goalPos);

            float directionalSpeed = 0;
            float jumpSpeed = 0;
           
            //Debug.Log(distance);

           


            if (Mathf.Abs( distance) >= 1)
            {

               

                if (KylesFunctions.IsNextToWall2D(agent.transform.localScale.x, agent.GetComponent<Collider2D>(), 0.1f, floor) && isOnGrounded)
                {
                    if (dotProd < 0)
                    {
                        jumpSpeed = 10;
                       // agent.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (dotProd > 0)
                    {
                        jumpSpeed = -10;
                        //agent.transform.localScale = new Vector3(-1, 1, 1);
                    }

                    agentRig.AddForce(new Vector2(jumpSpeed, jumpHeight));
                    //agentRig.AddForce(new Vector2(directionalSpeed, 0));
                }
                else if (isOnGrounded)
                {
                    if (dotProd < 0)
                    {
                        directionalSpeed = speed;
                        agent.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (dotProd > 0)
                    {
                        directionalSpeed = -speed;
                        agent.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    agentRig.AddForce(new Vector2(directionalSpeed, 0));
                }
                else if(!KylesFunctions.IsNextToWall2D(agent.transform.localScale.x, agent.GetComponent<Collider2D>(), 0.1f, floor))
                {
                    agentRig.velocity = (new Vector2(directionalSpeed * 0.5f, agentRig.velocity.y));
                }

            }
            else
            {
                
                path.RemoveAt(path.Count - 1);
           // agentRig.velocity = (new Vector2(0, 0));
            }
            //get grid x and y to make sure it gets to the correct position
            //while()





        }

        void playerPositionChanged()
        {

        }

        //public static bool PlayerInFrontOfAgent(Collider2D agentCollider, float distance, LayerMask mask)
        //{
        //    RaycastHit2D ray;
        //   ray = Physics2D.Raycast(agentCollider.bounds.center, Vector2.right, distance, mask);
        //    Debug.DrawRay(agentCollider.bounds.center, Vector2.right, Color.red);

        //    if(ray)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}


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
