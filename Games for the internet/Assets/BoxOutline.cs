using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOutline : MonoBehaviour
{
    public Color outlineColour1;
    public Color outLineColour2;
    private SpriteRenderer box;
    // Start is called before the first frame update
    void Start()
    {
        box = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.BoxCast(box.bounds.center, new Vector2(box.bounds.extents.x, box.bounds.extents.y), 0, Vector2.zero);
        Debug.DrawLine(box.bounds.center + new Vector3(-box.bounds.extents.x, box.bounds.extents.y), box.bounds.center + new Vector3(box.bounds.extents.x, box.bounds.extents.y), Color.red);
        Debug.DrawLine(box.bounds.center + new Vector3(-box.bounds.extents.x, -box.bounds.extents.y), box.bounds.center + new Vector3(box.bounds.extents.x, -box.bounds.extents.y), Color.red);
        Debug.DrawLine(box.bounds.center + new Vector3(-box.bounds.extents.x, -box.bounds.extents.y), box.bounds.center + new Vector3(-box.bounds.extents.x, box.bounds.extents.y), Color.red);
        Debug.DrawLine(box.bounds.center + new Vector3(box.bounds.extents.x, -box.bounds.extents.y), box.bounds.center + new Vector3(box.bounds.extents.x, box.bounds.extents.y), Color.red);
    }
}
