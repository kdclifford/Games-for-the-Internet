using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AiAnimationController.Utils
{
    public class AiAnimations
    {
       public static void Walk(Animator agentAnimation)
        {
            agentAnimation.SetInteger("Animation", 0);
        }

        public static void Attack(Animator agentAnimation)
        {
            agentAnimation.SetInteger("Animation", 1);
        }
    }
}