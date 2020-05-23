using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class FlatternGrass : MonoBehaviour
{

    public LayerMask grass;
    public float rayDist;

    private void Update()
    {
        RaycastHit2D hit;

        //cast ray under block to see if it touches grass
        hit = Physics2D.Raycast(gameObject.GetComponent<BoxCollider2D>().bounds.center, Vector2.down, rayDist, grass);
        Debug.DrawRay(gameObject.GetComponent<BoxCollider2D>().bounds.center, Vector3.down, Color.red);

        if (hit)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newPos.y = newPos.y - 0.5f;
            //Set tile that block collides with to null
            hit.collider.gameObject.GetComponent<Tilemap>().SetTile(hit.collider.gameObject.GetComponent<Tilemap>().WorldToCell(newPos), null);

        }

    }
}

