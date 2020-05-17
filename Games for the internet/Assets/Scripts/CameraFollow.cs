using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    public float cameraHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newCamPos = new Vector3();
        newCamPos.x = followObject.transform.position.x;
        newCamPos.y = followObject.transform.position.y + cameraHeight;
        newCamPos.z = transform.position.z;

        transform.position = newCamPos;

        

    }
}
