using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public static bool isGrounded2D(CapsuleCollider2D agentCollider, float rayFloorDistance, LayerMask groundMask)
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
                    //If hitting floor layer then node is not accesible = 0
                    if (Physics2D.Raycast(new Vector2(gridxy.x + 0.5f, gridxy.y + 0.5f), Vector2.zero, 0.0f, floorMask))
                    {
                        grid.SetValue(grid.GetWorldPosition(x, y), 0);
                    }
                    //This is a air block but is above the walkable ground = 1
                    else if (Physics2D.Raycast(new Vector2(gridxy.x + 0.5f, gridxy.y + 0.5f), Vector2.down, 0.6f, floorMask))
                    {
                        grid.SetValue(grid.GetWorldPosition(x, y), 1);
                    }
                    //Air equals 2
                    else
                    {
                        grid.SetValue(grid.GetWorldPosition(x, y), 2);
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
        public static void AStar(Grid terrainMap, int startX, int startY, int goalX, int goalY, int agentHeight, bool left)
        {
            List<CNode> openList = new List<CNode>();
            List<CNode> closedList = new List<CNode>();

            CNode startNode = new CNode();
            startNode.x = startX;
            startNode.y = startY;
            startNode.heuristic = GetHeuristic(startNode.x, startNode.y, goalX, goalY);
            startNode.cost = GetHeuristic(startNode.x, startNode.y, startX, startY);
            startNode.fCost = startNode.cost + startNode.heuristic;

            openList.Add(startNode);


            while (openList.Count > 0 | openList[0].x != goalX && openList[0].y != goalY)
            {
                openList = GenerateNodes(ref openList, ref closedList, new Vector2(startX, startY), new Vector2(goalX, goalY), terrainMap);
                closedList.Add(openList[0]);
                openList.RemoveAt(0);
                openList = openList.OrderBy(go => go.fCost).ToList();


            }






        }

        public static List<CNode> GenerateNodes(ref List<CNode> openList, ref List<CNode> closedList, Vector2 start, Vector2 goal, Grid terrainMap)
        {
            if (openList.Count != 0)
            {
                //Coords of First element in openlist
                CNode currentNode = new CNode();
                CNode newNode = new CNode();
                currentNode.x = openList[0].x;
                currentNode.y = openList[0].y;
                currentNode.heuristic = GetHeuristic(currentNode.x, currentNode.y, Mathf.FloorToInt(goal.x), Mathf.FloorToInt(goal.y));
                currentNode.cost = GetHeuristic(currentNode.x, currentNode.y, Mathf.FloorToInt(start.x), Mathf.FloorToInt(start.y));
                currentNode.fCost = currentNode.cost + currentNode.heuristic;

                //North
                if (terrainMap.GetValue(currentNode.x, currentNode.y + 1) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x;
                    newNode.y = currentNode.y + 1;
                    newNode.fCost = currentNode.heuristic + currentNode.cost;
                    newNode.parentNode = currentNode;
                    if (!CheckListForLowerScore(ref closedList, ref openList, newNode))
                    {
                        openList.Add(newNode);
                    }
                }

                //East
                if (terrainMap.GetValue(currentNode.x + 1, currentNode.y) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x + 1;
                    newNode.y = currentNode.y;
                    newNode.fCost = currentNode.heuristic + currentNode.cost;
                    newNode.parentNode = currentNode;
                    if (!CheckListForLowerScore(ref closedList, ref openList, newNode))
                    {
                        openList.Add(newNode);
                    }
                }

                //South
                if (terrainMap.GetValue(currentNode.x, currentNode.y - 1) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x;
                    newNode.y = currentNode.y - 1;
                    newNode.fCost = currentNode.heuristic + currentNode.cost;
                    newNode.parentNode = currentNode;
                    if (!CheckListForLowerScore(ref closedList, ref openList, newNode))
                    {
                        openList.Add(newNode);
                    }
                    // CheckListForLowerScore(ref openList, ref closedList, newNode);
                }

                //West
                if (terrainMap.GetValue(currentNode.x - 1, currentNode.y) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x - 1;
                    newNode.y = currentNode.y;
                    newNode.fCost = currentNode.heuristic + currentNode.cost;
                    newNode.parentNode = currentNode;
                    if (!CheckListForLowerScore(ref closedList, ref openList, newNode))
                    {
                        openList.Add(newNode);
                    }
                }


            }

            return openList;
        }

        public static int GetHeuristic(int x, int y, int x2, int y2)
        {
            int heuristic = (Mathf.Abs(x) + Mathf.Abs(y)) - (Mathf.Abs(x2) + Mathf.Abs(y2));

            return heuristic;
        }

        public static bool CheckListForLowerScore(ref List<CNode> list, ref List<CNode> newList, CNode newNode)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].x == newNode.x && list[i].y == newNode.y)
                {
                    if (list[i].fCost < newNode.fCost)
                    {
                        newList.Add(list[i]);
                        list.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

    }
}