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


        public static void MoveAgentTo(GameObject agent, Vector2Int agentGridPos, float speed, float jumpHeight, Vector2Int goalPos, List<LayerMask> floor, ref List<CNode> path, AudioManager audio)
        {
            Rigidbody2D agentRig = agent.GetComponent<Rigidbody2D>();
            //Vector2Int agentPos = KylesFunctions.GetXY()
           
            
            // bool isOnGrounded = KylesFunctions.isGrounded2D(agent.GetComponent<Collider2D>(), 0.1f, floor, agent);
            // bool isOnGrounded = Physics2D.Raycast(agent.GetComponent<Collider2D>().bounds.center, Vector2.down, agent.GetComponent<Collider2D>().bounds.extents.y + 0.1f, 11);
            bool isOnGrounded = AiMaths.raycast(agent.GetComponent<Collider2D>(), floor, 0);


            // agent.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 0));

            float dotProd = agentGridPos.x - goalPos.x;
            float distance = Vector2Int.Distance(agentGridPos, goalPos);
            if (path.Count >= 2)
            {

                float nextDistance = Vector2Int.Distance(agentGridPos, new Vector2Int(path[path.Count - 2].x, path[path.Count - 2].y));
                if(nextDistance < distance)
                {
                    distance = nextDistance;
                    path.RemoveAt(path.Count - 1);
                    goalPos = new Vector2Int(path[path.Count - 1].x, path[path.Count - 1].y);
                    dotProd = agentGridPos.x - goalPos.x;
                }
            }
            float directionalSpeed = 10;
            float jumpSpeed = 10;

            //Debug.Log(distance);

            if (dotProd < 0)
            {

                agent.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (dotProd > 0)
            {

                agent.transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Mathf.Abs(distance) >= 1)
            {



                if (KylesFunctions.IsNextToWall2D(agent.transform.localScale.x, agent.GetComponent<Collider2D>(), 0.5f, floor) && isOnGrounded)
                {

                    audio.Play("MantisJump", agent);
                    agentRig.velocity = (new Vector2(0, jumpHeight));
                    //agentRig.AddForce(new Vector2(speed * (int)agent.transform.localScale.x, agentRig.velocity.y));
                    //agentRig.AddForce(new Vector2(directionalSpeed, 0));
                }
                else if(isOnGrounded)
                {

                    agentRig.velocity = (new Vector2(directionalSpeed * (int)agent.transform.localScale.x, agentRig.velocity.y));

                }
                else
                {
                    agentRig.velocity = (new Vector2(1 * (int)agent.transform.localScale.x, agentRig.velocity.y));
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

        public static bool raycast(Collider2D col, List<LayerMask> mask, float distance)
        {
            RaycastHit2D hit;
            Color colour;
            foreach (LayerMask node in mask)
            {
                hit = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.1f, node);
                // hit = Physics2D.BoxCast(col.bounds.center, new Vector3(col.bounds.size.x, col.bounds.extents.y, 0), 0, Vector2.down, col.bounds.extents.y + 0.1f, node);


                Debug.DrawRay(col.bounds.center, Vector2.down * (col.bounds.extents.y + 0.1f), Color.red);


                if (hit)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool raycastSides(Collider2D col, List<LayerMask> mask, int scale, GameObject currentObject)
        {
            RaycastHit2D hit;
           
            foreach (LayerMask node in mask)
            {
                if (scale == 1)
                {

                    hit = Physics2D.Raycast(col.bounds.center, Vector2.right, col.bounds.extents.y + 0.1f, node);
                }
                else
                {
                    hit = Physics2D.Raycast(col.bounds.center, Vector2.left, col.bounds.extents.y + 0.1f, node);
                }
               
                // hit = Physics2D.BoxCast(col.bounds.center, new Vector3(col.bounds.size.x, col.bounds.extents.y, 0), 0, Vector2.down, col.bounds.extents.y + 0.1f, node);


                //Debug.DrawRay(col.bounds.center, Vector2.down * (col.bounds.extents.y + 0.1f), Color.red);


                if (hit)
                {
                    if (hit.collider.gameObject != currentObject)
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }






    }
}
