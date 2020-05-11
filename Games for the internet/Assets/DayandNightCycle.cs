using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayandNightCycle : MonoBehaviour
{
    public GameObject camera;
    public float daySpeed;
    public float startPos;
    private GameObject moon;
    // Start is called before the first frame update
    void Start()
    {
        startPos = camera.transform.position.x;
        transform.position = new Vector3(camera.transform.position.x, transform.position.y, transform.position.z);
        moon = transform.GetChild(0).gameObject;

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 24f));
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        float moonRotation = camera.transform.position.x * daySpeed;
        transform.position = new Vector3(camera.transform.position.x, transform.position.y, transform.position.z);
        //transform.position = new Vector3(startPos + moveDistance, transform.position.y, transform.position.z);
        float currentRotation = transform.rotation.z;
        //Add current rotation to rotation rate to get new rotation
        Quaternion rotation = Quaternion.Euler(0, 0, (currentRotation + moonRotation ));
        transform.rotation = rotation;

        //if (moon.transform.position.y < -4f)
        //{
        //    transform.Rotate(0, 0, (daySpeed * 100) * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Rotate(0, 0, daySpeed * Time.deltaTime);
        //}
        moon.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z + moon.transform.localRotation.z);


        Quaternion rotationMin = Quaternion.Euler(new Vector3(0f, 0f, -30f));
        Quaternion rotationMax = Quaternion.Euler(new Vector3(0f, 0f, 30f));
        Quaternion testrotation = transform.rotation;

        Quaternion newMaxRot = Quaternion.Euler(new Vector3(0f, 0f, 24f));
        Quaternion newMinRot = Quaternion.Euler(new Vector3(0f, 0f, -29f));
        float dotProd = moon.transform.position.x - camera.transform.position.x;

     if(testrotation.z < rotationMin.z && dotProd > 0)
        {
            transform.rotation = rotationMax;
        }

        Debug.Log(transform.eulerAngles.z);


    }
}
