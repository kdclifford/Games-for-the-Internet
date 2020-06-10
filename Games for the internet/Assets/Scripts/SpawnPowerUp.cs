using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    public GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject powerUp = (GameObject)Instantiate(powerUpPrefab);
        powerUp.transform.position = transform.position;
        powerUp.transform.position = new Vector3(powerUp.transform.position.x, powerUp.transform.position.y + 0.1f, powerUp.transform.position.z);
    }

  
}
