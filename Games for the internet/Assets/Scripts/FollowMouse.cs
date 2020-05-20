using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Vector2 MouseOffset = new Vector2();
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition + MouseOffset);
        newPos.z = 0;
        transform.position = newPos;
    }
}
