using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffect;
    public GameObject camPos;
    private float startPos, bgLength;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        bgLength = GetComponent<SpriteRenderer>().bounds.size.x;        
    }

    // Update is called once per frame
    void Update()
    {
        float oldDistance = (camPos.transform.position.x * (1 - parallaxEffect));
        float moveDistance = (camPos.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + moveDistance, transform.position.y, transform.position.z);

        if(oldDistance > startPos + bgLength)
        {
            startPos += bgLength;
        }
        else if(oldDistance < startPos - bgLength)
        {
            startPos -= bgLength;
        }
    }
}
