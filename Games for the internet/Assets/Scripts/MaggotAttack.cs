using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotAttack : MonoBehaviour
{
    public bool attack = false;
    public GameObject blobPrefab;

    public void SpawnBlob()
    {
        attack = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (attack)
        {
            GameObject blob = (GameObject)Instantiate(blobPrefab);
            blob.transform.position = transform.position;
            blob.GetComponent<MoveAcidBlob>().direction = (int)transform.localScale.x;
            attack = false;
        }

    }
}
