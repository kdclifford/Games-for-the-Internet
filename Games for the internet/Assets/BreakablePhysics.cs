using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePhysics : MonoBehaviour
{
    public float torque;
    public Vector2 direction;

    private Rigidbody2D objectRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        objectRigidbody = GetComponent<Rigidbody2D>();
        objectRigidbody.AddForce(direction);
        objectRigidbody.AddTorque(torque);
    }

   
}
