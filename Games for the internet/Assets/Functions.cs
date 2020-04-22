
//static private bool isGrounded()
//{
//    float extraHieght = 0.1f;
//    RaycastHit2D hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraHieght, floorMask);
//    Color rayColour;
//    if (hit.collider != null)
//    {
//        rayColour = Color.green;
//    }
//    else
//    {
//        rayColour = Color.red;
//    }
//    Debug.DrawRay(playerCollider.bounds.center + new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraHieght), rayColour);
//    Debug.DrawRay(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraHieght), rayColour);
//    Debug.DrawRay(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x, playerCollider.bounds.extents.y + extraHieght), Vector3.right * (playerCollider.bounds.extents.x * 2), rayColour);
//    Debug.Log(hit.collider);

//    return hit.collider != null;

//}