using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;

public class MaggotAi : MonoBehaviour
{
    Rigidbody2D agentRig;
     Collider2D agentCol;
    public List<LayerMask> mask;

    // Start is called before the first frame update
    void Start()
    {
        agentRig = GetComponent<Rigidbody2D>();
        agentCol = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(KylesFunctions.maggotTrapped(agentCol, mask))
        {
            GetComponent<DestroyBlock>().die = true;
        }


        if (KylesFunctions.maggotRaycasts(agentCol, (int)transform.localScale.x, mask))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            GetComponent<MaggotAttack>().attack = true;
        }
        
            agentRig.velocity = new Vector2(3 * (int)transform.localScale.x, agentRig.velocity.y);
        
    }
}
