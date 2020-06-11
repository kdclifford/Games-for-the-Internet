using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{    
    private Movement playerPowerUps;
    private Rigidbody2D blockRig;
    private Collider2D blockCol;

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
            if (!playerPowerUps.block)
            {
                blockRig.constraints = RigidbodyConstraints2D.FreezePositionX;
                blockRig.constraints = RigidbodyConstraints2D.FreezeRotation;
                //blockRig.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            else
            {
                blockRig.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    bool CheckForMantis()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(blockCol.bounds.center , Vector2.right, blockCol.bounds.extents.x + 0.1f, 20);
        if(hit)
        {
            return true;
        }
        else
        {
            hit = Physics2D.Raycast(blockCol.bounds.center, Vector2.left, blockCol.bounds.extents.x + 0.1f, 20);
            if (hit)
            {
                return true;
            }
        }       
            return false;
}


}
