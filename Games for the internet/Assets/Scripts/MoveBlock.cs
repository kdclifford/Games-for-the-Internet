using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{    
    private Movement playerPowerUps;
    private Rigidbody2D blockRig;
    private Collider2D blockCol;
    public LayerMask mantis;
    private void Start()
    {
        playerPowerUps = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        blockRig = GetComponent<Rigidbody2D>();
        blockCol = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    void Update()
    {

        if (CheckForMantis())
        {
            blockRig.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            if (playerPowerUps.block)
            {
                
                blockRig.constraints = RigidbodyConstraints2D.FreezeRotation;
                //blockRig.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            else
            {
                blockRig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;                
            }
        }
    }

    Color right = Color.red;
    Color left = Color.red;
    bool CheckForMantis()
    {        
        RaycastHit2D hit;
        hit = Physics2D.Raycast(blockCol.bounds.center , Vector2.right, blockCol.bounds.extents.x + 0.5f, mantis);
        Debug.DrawRay(blockCol.bounds.center, Vector2.right * (blockCol.bounds.extents.x + 0.5f), right);
        Debug.DrawRay(blockCol.bounds.center, Vector2.left * (blockCol.bounds.extents.x + 0.5f), left);
        //Debug.Log(hit.collider.name);

        if(hit)
        {
            right = Color.green;
            return true;
        }
        else
        {
            hit = Physics2D.Raycast(blockCol.bounds.center, Vector2.left, blockCol.bounds.extents.x + 0.5f, mantis);
           
            if (hit)
            {
                left = Color.green;
                return true;
            }
        }
        left = Color.red;
        right = Color.red;
            return false;
}


}
