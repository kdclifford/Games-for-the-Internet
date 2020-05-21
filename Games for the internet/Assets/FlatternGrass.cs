using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class FlatternGrass : MonoBehaviour
{
    public Tilemap foreground;
    public Tile tile;


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.layer == 15)
    //    {

    public LayerMask grass;
    public float rayDist;

    private void Update()
    {
      //  Physics2D.IgnoreLayerCollision(13,  15);

        RaycastHit2D hit;
        // hit = Physics2D.BoxCast(gameObject.GetComponent<BoxCollider2D>().bounds.center, gameObject.GetComponent<BoxCollider2D>().bounds.size, 0f, Vector2.zero, 0f, 15);
       
        hit = Physics2D.Raycast(gameObject.GetComponent<BoxCollider2D>().bounds.center, Vector2.down, rayDist, grass);
        // Debug.DrawRay(gameObject.GetComponent<BoxCollider2D>().bounds.center + new Vector3(gameObject.GetComponent<BoxCollider2D>().bounds.extents.x, 0), Vector2.down * (gameObject.GetComponent<BoxCollider2D>().bounds.extents.y + rayDist), Color.red);
        // Debug.DrawRay(gameObject.GetComponent<BoxCollider2D>().bounds.center - new Vector3(gameObject.GetComponent<BoxCollider2D>().bounds.extents.x, 0), Vector2.down * (gameObject.GetComponent<BoxCollider2D>().bounds.extents.y + rayDist), Color.red);
        // Debug.DrawRay(gameObject.GetComponent<BoxCollider2D>().bounds.center - new Vector3(gameObject.GetComponent<BoxCollider2D>().bounds.extents.x, gameObject.GetComponent<BoxCollider2D>().bounds.extents.y + rayDist), Vector3.right * (gameObject.GetComponent<BoxCollider2D>().bounds.extents.x * 2), Color.red);
         Debug.DrawRay(gameObject.GetComponent<BoxCollider2D>().bounds.center, Vector3.down, Color.red);

        if (hit)
        {
        Debug.Log(hit.collider.name);
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newPos.y = newPos.y - 0.5f;
           // newPos.x = newPos.x - 0.5f;
            foreground.gameObject.GetComponent<Tilemap>().SetTile(foreground.WorldToCell(newPos), null);
           // newPos.x = newPos.x + 1.0f;
           // foreground.gameObject.GetComponent<Tilemap>().SetTile(foreground.WorldToCell(newPos), null);
        }
            //Destroy(collision.gameObject);
        }
    }

