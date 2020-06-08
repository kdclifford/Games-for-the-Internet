using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;

public class test : MonoBehaviour
{
   

    public float width;

    public float height;

    public Vector3 origin;

    public direction dir;

    public float distance;

    public LayerMask mask;
    // Update is called once per frame
    void Update()
    {
        Vector2 neworg = origin + transform.position;
        Vector2 directionvect = new Vector2();
        
        switch(dir)
        {
            case direction.down:
                directionvect = Vector2.down;
                break;
            case direction.up:
                directionvect = Vector2.up;
                break;
            case direction.right:
                directionvect = Vector2.right;
                break;
            case direction.left:
                directionvect = Vector2.left;
                break;
        }
        

        KylesFunctions.BoxCastDebug(height, width, neworg, directionvect, distance, mask);
    }
}
