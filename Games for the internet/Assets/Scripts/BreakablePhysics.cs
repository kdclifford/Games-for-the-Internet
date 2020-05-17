using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePhysics : MonoBehaviour
{
    public float torque;
    public Vector2 direction;
    public float timer = 30;
    public float currentTime = 0;
    private Rigidbody2D objectRigidbody;
        private Color newAlpha;
    private Color oldAlpha;
    // Start is called before the first frame update
    void Start()
    {
        objectRigidbody = GetComponent<Rigidbody2D>();
        objectRigidbody.AddForce(direction);
        objectRigidbody.AddTorque(Random.Range(-torque * 5, torque * 5));
       

        newAlpha = gameObject.GetComponent<SpriteRenderer>().color;
        oldAlpha = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
       // timer -= Time.deltaTime;
        currentTime += Time.deltaTime;
        if(newAlpha.a <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
        newAlpha.a = Mathf.Lerp(oldAlpha.a, 0, currentTime / timer);
        gameObject.GetComponent<SpriteRenderer>().color = newAlpha;
        }
    }



}
