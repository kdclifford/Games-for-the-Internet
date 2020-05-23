using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator agentAnimation;

    private void Update()
    {
        Attack();
    }

    void Walk()
    {
        agentAnimation.SetInteger("Animation", 0);
    }

    void Attack()
    {
        agentAnimation.SetInteger("Animation", 1);
    }

}
