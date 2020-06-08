using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;

public class MaggotAi : MonoBehaviour
{
    Rigidbody2D agentRig;
    public GameObject agentCol;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        agentRig = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (KylesFunctions.IsNextToWall2D(transform.localScale.x, agentCol.GetComponent<Collider2D>(), 0.1f, mask))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            GetComponent<MaggotAttack>().attack = true;
        }
        
            agentRig.velocity = new Vector2(5 * (int)transform.localScale.x, 0);
        
    }
}
