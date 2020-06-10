using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgentInfo : MonoBehaviour
{
    public bool canFly = false;
    public float jumpHeight = 0;
    public float speed = 0;
    public int health = 0;
    public float sightRange = 0;
    public bool isJumping = false;

    private void Update()
    {
        if(canFly)
        {
            jumpHeight = 0;
        }
    }
}
