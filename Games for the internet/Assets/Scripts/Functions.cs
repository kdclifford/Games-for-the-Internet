using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Functions.Utils
{

    public class KylesFunctions
    {

    public const int sortingOrderDefault = 5000;

        // Create Text in the World
        public static TextMesh CreateText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
        {
            if (color == null) color = Color.white;
            return CreateText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }

        // Create Text in the World
        public static TextMesh CreateText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }

        //Check if Grounded
        public static bool isGrounded2D(CapsuleCollider2D agentCollider, float rayFloorDistance, LayerMask groundMask )
        {
            RaycastHit2D hit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size, 0f, Vector2.down, rayFloorDistance, groundMask);
            Color rayColour;
            if (hit.collider != null)
            {
                rayColour = Color.green;
            }
            else
            {
                rayColour = Color.red;
            }
            Debug.DrawRay(agentCollider.bounds.center + new Vector3(agentCollider.bounds.extents.x, 0), Vector2.down * (agentCollider.bounds.extents.y + rayFloorDistance), rayColour);
            Debug.DrawRay(agentCollider.bounds.center - new Vector3(agentCollider.bounds.extents.x, 0), Vector2.down * (agentCollider.bounds.extents.y + rayFloorDistance), rayColour);
            Debug.DrawRay(agentCollider.bounds.center - new Vector3(agentCollider.bounds.extents.x, agentCollider.bounds.extents.y + rayFloorDistance), Vector3.right * (agentCollider.bounds.extents.x * 2), rayColour);
            Debug.Log(hit.collider);

            return hit.collider != null;
        }

        //Check if Agent is next to a wall
        public static bool IsNextToWall2D(float scale, CapsuleCollider2D agentCollider, float rayWallDistance, LayerMask wallMask)
        {
            RaycastHit2D hit;
            if (scale == 1)
            {
                hit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size, 0f, Vector2.right, rayWallDistance, wallMask);
            }
            else
            {
                hit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size, 0f, Vector2.left, rayWallDistance, wallMask);
            }


            Color rayColour;
            if (hit.collider != null)
            {
                rayColour = Color.green;
            }
            else
            {
                rayColour = Color.red;
            }

            if (scale == 1)
            {
                Debug.DrawRay(agentCollider.bounds.center + new Vector3(agentCollider.bounds.extents.x, 0), Vector3.right * rayWallDistance, rayColour);
            }
            else
            {
                Debug.DrawRay(agentCollider.bounds.center - new Vector3(agentCollider.bounds.extents.x, 0), Vector3.left * rayWallDistance, rayColour);

            }

            Debug.Log(hit.collider);

            return hit.collider != null;

        }

        //Set all the Grid Values to pathable and non pathable
        public static Grid GridValues(Grid grid, LayerMask floorMask)
        {

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    Vector2 gridxy = grid.GetWorldPosition(x, y);
                    //If hitting floor layer then node is not accesible
                    if (Physics2D.Raycast(new Vector2(gridxy.x + 0.5f, gridxy.y + 0.5f), Vector2.zero, 0.0f, floorMask))
                    {
                        grid.SetValue(grid.GetWorldPosition(x, y), 0);
                    }
                    else
                    {
                            grid.SetValue(grid.GetWorldPosition(x, y), 1);
                    }



                }

            }

            return grid;
        }

        //Get Grid X and Y from Object Position
        public static void GetXY(Vector3 worldPosition, Vector3 originPosition, float cellSize, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        //AStar 

        public static void AStar(Grid terrainMap, CNode start, CNode goal, int agentHeight, bool left)
        {
            List<CNodeList> openList = new List<CNodeList>();
            List<CNodeList> closedList = new List<CNodeList>();

            CNodeList startNode = new CNodeList();
            startNode.Node.x = start.x;
            startNode.Node.y = start.y;

            openList.Add(startNode);


            while(openList.Count > 0)
            {
               openList = GenerateNodes(openList, terrainMap);



            }






        }

        public static List<CNodeList> GenerateNodes(List<CNodeList> openList, Grid terrainMap)
        {
            if(openList.Count != 0)
            {
                //Coords of First element in openlist
                CNode currentNode = new CNode();
                CNodeList newNode = new CNodeList();
                currentNode.x = openList[0].Node.x;
                currentNode.y = openList[0].Node.y;
                currentNode.score = openList[0].Node.score;
               

                //North
                if (terrainMap.GetValue(currentNode.x, currentNode.y + 1) != 0)
                {
                    newNode = new CNodeList();
                    newNode.Node.x = currentNode.x;
                    newNode.Node.y = currentNode.y + 1;
                    newNode.Node.score = currentNode.score + currentNode.cost;
                    openList.Add(newNode);
                }

                //East
                if (terrainMap.GetValue(currentNode.x + 1, currentNode.y) != 0)
                {
                    newNode = new CNodeList();
                    newNode.Node.x = currentNode.x + 1;
                    newNode.Node.y = currentNode.y;
                    newNode.Node.score = currentNode.score + currentNode.cost;
                }

                //South
                if (terrainMap.GetValue(currentNode.x, currentNode.y - 1) != 0)
                {
                    newNode = new CNodeList();
                    newNode.Node.x = currentNode.x;
                    newNode.Node.y = currentNode.y - 1;
                    newNode.Node.score = currentNode.score + currentNode.cost;
                }

                //West
                if (terrainMap.GetValue(currentNode.x - 1, currentNode.y) != 0)
                {
                    newNode = new CNodeList();
                    newNode.Node.x = currentNode.x - 1;
                    newNode.Node.y = currentNode.y;
                    newNode.Node.score = currentNode.score + currentNode.cost;
                }


            }

            return openList;
        }



    }
}