﻿using System.Collections;
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

        // check if object is grounded and if only has one object mask
        public static bool isGrounded2D(Collider2D objectCollider, float distance, LayerMask mask)
        {
            //create list with the passed over mask
            List<LayerMask> newFloor = new List<LayerMask>();
            newFloor.Add(mask);
            return isGrounded2D(objectCollider, distance, newFloor);
        }
        //Check if object is Grounded
        public static bool isGrounded2D(Collider2D objectCollider, float distance, List<LayerMask> maskList)
        {
            foreach (LayerMask node in maskList)
            {

                if (BoxCastDebug(objectCollider.bounds.extents.y * 2, objectCollider.bounds.extents.x * 2, objectCollider.bounds.center, Vector2.down, distance, node))
                {
                    return true;
                }
            }
            return false;
        }

        // CHECKS TO SEE IF ITS HITTING ITS SELF
        // check if object is grounded and if only has one object mask
        public static bool isGrounded2D(Collider2D objectCollider, float distance, LayerMask mask, GameObject currentObject)
        {
            //create list with the passed over mask
            List<LayerMask> newFloor = new List<LayerMask>();
            newFloor.Add(mask);
            return isGrounded2D(objectCollider, distance, newFloor, currentObject);
        }
        //Check if object is Grounded
        public static bool isGrounded2D(Collider2D objectCollider, float distance, List<LayerMask> maskList, GameObject currentObject)
        {
            foreach (LayerMask node in maskList)
            {

                if (BoxCastDebug(objectCollider.bounds.extents.y * 2, objectCollider.bounds.extents.x * 2, objectCollider.bounds.center, Vector2.down, distance, node, currentObject))
                {
                    return true;
                }
            }
            return false;
        }



        //Check if Agent is next to a wall
        public static bool IsNextToWall2D(float scale, Collider2D agentCollider, float rayWallDistance, LayerMask wallMask)
        {
            RaycastHit2D hit;
            if (scale == 1)
            {
                hit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size - new Vector3 (0, agentCollider.bounds.extents.y), 0f, Vector2.right, rayWallDistance, wallMask);
            }
            else
            {
                hit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size - new Vector3(0, agentCollider.bounds.extents.y), 0f, Vector2.left, rayWallDistance, wallMask);
            }


            Color rayColour;
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                rayColour = Color.green;
            }
            else
            {
                rayColour = Color.red;
            }



            if (scale == 1)
            {
                //Debug.DrawRay(agentCollider.bounds.center + new Vector3(agentCollider.bounds.extents.x, 0), Vector3.right * rayWallDistance, rayColour);
            }
            else
            {
                //Debug.DrawRay(agentCollider.bounds.center - new Vector3(agentCollider.bounds.extents.x, 0), Vector3.left * rayWallDistance, rayColour);

            }

            //Debug.Log(hit.collider);

            return hit.collider != null;

        }

        public static bool IsNextToWall2D(float scale, Collider2D agentCollider, float rayWallDistance, LayerMask wallMask, GameObject currentAgent)
        {
            RaycastHit2D hit;
            if (scale == 1)
            {
                hit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size - new Vector3(0, agentCollider.bounds.extents.y), 0f, Vector2.right, rayWallDistance, wallMask);
            }
            else
            {
                hit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size - new Vector3(0, agentCollider.bounds.extents.y), 0f, Vector2.left, rayWallDistance, wallMask);
            }


            Color rayColour;

            if (hit.collider != null)
            {
                if (hit.collider.gameObject != currentAgent)
                {
                    Debug.Log(hit.collider.name);

                    rayColour = Color.green;
                }
                else
                {
                return false;
                }
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

            //Debug.Log(hit.collider);

            return hit.collider != null;

        }


        public static bool IsNextToWall2D(float scale, Collider2D agentCollider, float rayWallDistance, List<LayerMask> maskList, GameObject currentAgent)
        {
            foreach (LayerMask node in maskList)
            {

                if (IsNextToWall2D(scale, agentCollider, rayWallDistance, node, currentAgent))
                {
                    return true;
                }


            }
            return false;
        }

        public static bool IsNextToWall2D(float scale, Collider2D agentCollider, float rayWallDistance, List<LayerMask> maskList)
        {
            foreach (LayerMask node in maskList)
            {

                if (IsNextToWall2D(scale, agentCollider, rayWallDistance, node))
                {
                    return true;
                }


            }
            return false;
        }

        //Set all the Grid Values to pathable and non pathable
        public static Grid GridValues(Grid grid, LayerMask floorMask, float playerHeight, Color gridColour)
        {

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    Vector2 gridxy = grid.GetWorldPosition(x, y);
                    //If hitting floor layer then node is not accesible = 0
                    if (Physics2D.Raycast(new Vector2(gridxy.x + 0.5f, gridxy.y + 0.5f), Vector2.zero, 0.0f, floorMask))
                    {
                        grid.SetValue(grid.GetWorldPosition(x, y), 0, gridColour);
                    }
                    //This is a air block but is above the walkable ground = 1
                    else if (Physics2D.Raycast(new Vector2(gridxy.x + 0.5f, gridxy.y + 0.5f), Vector2.down, 2.6f, floorMask))
                    {
                        grid.SetValue(grid.GetWorldPosition(x, y), 1, gridColour);
                    }
                    //else if (Physics2D.Raycast(new Vector2(gridxy.x + 0.5f, gridxy.y + 0.5f), Vector2.down, 0.1f + playerHeight, floorMask))
                    //{
                    //    grid.SetValue(grid.GetWorldPosition(x, y), 1, gridColour);
                    //}
                    //Air equals 2
                    else
                    {
                        grid.SetValue(grid.GetWorldPosition(x, y), 0, gridColour);
                    }
                }
            }
            return grid;
        }

        //Get Grid X and Y from Object Position
        public static void GetXY(Vector3 worldPosition, Vector3 originPosition, float cellSize, out Vector2Int XY)
        {
            XY = new Vector2Int(Mathf.FloorToInt((worldPosition - originPosition).x / cellSize), Mathf.FloorToInt((worldPosition - originPosition).y / cellSize));

        }

        //Get Grid X and Y from Object Position
        public static void GetXY(Vector3 worldPosition, Vector3 originPosition, float cellSize, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        //AStar 
        public static void AStar(Grid terrainMap, int startX, int startY, int goalX, int goalY, int agentHeight, bool left, ref List<CNode> path)
        {
            List<CNode> openList = new List<CNode>();
            List<CNode> closedList = new List<CNode>();


            CNode startNode = new CNode();
            startNode.x = startX;
            startNode.y = startY;
            startNode.parentNode = new CNode();
            startNode.parentNode.x = terrainMap.GetHeight() + terrainMap.GetWidth();
            startNode.parentNode.y = terrainMap.GetHeight() + terrainMap.GetWidth();
            startNode.heuristic = GetHeuristic(startNode.x, startNode.y, goalX, goalY);
            startNode.cost = GetHeuristic(startNode.x, startNode.y, startX, startY);
            startNode.fCost = startNode.cost + startNode.heuristic;

            openList.Add(startNode);


            while (openList.Count > 0)
            {

                //Coords of First element in openlist
                CNode currentNode = new CNode();

                currentNode.x = openList[0].x;
                currentNode.y = openList[0].y;
                currentNode.parentNode = openList[0].parentNode;

                currentNode.heuristic = GetHeuristic(currentNode.x, currentNode.y, Mathf.FloorToInt(goalX), Mathf.FloorToInt(goalY));
                currentNode.cost = GetHeuristic(currentNode.x, currentNode.y, Mathf.FloorToInt(startX), Mathf.FloorToInt(startY));
                currentNode.stepAmount = openList[0].stepAmount;
                currentNode.fCost = currentNode.cost + currentNode.heuristic + currentNode.stepAmount;


                openList = GenerateNodes(ref openList, ref closedList, new Vector2(startX, startY), new Vector2(goalX, goalY), terrainMap, ref currentNode);
                //closedList.Add(openList[0]);
                //openList.RemoveAt(0);
                openList = openList.OrderBy(go => go.fCost).ToList();




                if (currentNode != null && currentNode.x == goalX && currentNode.y == goalY)
                {
                    path.Clear();
                    //List<CNode> path = new List<CNode>();
                    path.Add(currentNode);

                    int GoalparentPos = terrainMap.GetHeight() + terrainMap.GetWidth();

                    while (path[path.Count - 1].parentNode.x != GoalparentPos && path[path.Count - 1].parentNode.y != GoalparentPos)
                    {
                        path.Add(path[path.Count - 1].parentNode);
                        // Debug.Log(path[path.Count - 1].x + "  " + path[path.Count - 1].y);
                    }

                    Debug.Log("Path Found");

                    //path.RemoveAt(0);
                    path.RemoveAt(path.Count - 1);
                }


            }






        }

        // genertate all accessible nodes from parent node
        public static List<CNode> GenerateNodes(ref List<CNode> openList, ref List<CNode> closedList, Vector2 start, Vector2 goal, Grid terrainMap, ref CNode currentNode)
        {
            if (openList.Count != 0)
            {
                //remove current node
                openList.RemoveAt(0);

                CNode newNode = new CNode();


                //North

                if (terrainMap.GetValue(currentNode.x, currentNode.y + 1) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x;
                    newNode.y = currentNode.y + 1;
                    PopulateNode(ref newNode, ref currentNode, ref openList, ref closedList, start, goal);

                    CheckLeftRight(terrainMap, ref newNode, ref currentNode, ref openList, ref closedList, start, goal);

                }


                //East

                if (terrainMap.GetValue(currentNode.x + 1, currentNode.y) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x + 1;
                    newNode.y = currentNode.y;
                    PopulateNode(ref newNode, ref currentNode, ref openList, ref closedList, start, goal);
                    CheckUpDown(terrainMap, ref newNode, ref currentNode, ref openList, ref closedList, start, goal);
                }


                //South

                if (terrainMap.GetValue(currentNode.x, currentNode.y - 1) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x;
                    newNode.y = currentNode.y - 1;
                    PopulateNode(ref newNode, ref currentNode, ref openList, ref closedList, start, goal);
                    CheckLeftRight(terrainMap, ref newNode, ref currentNode, ref openList, ref closedList, start, goal);
                }


                //West

                if (terrainMap.GetValue(currentNode.x - 1, currentNode.y) != 0)
                {
                    newNode = new CNode();
                    newNode.x = currentNode.x - 1;
                    newNode.y = currentNode.y;
                    PopulateNode(ref newNode, ref currentNode, ref openList, ref closedList, start, goal);
                    CheckUpDown(terrainMap, ref newNode, ref currentNode, ref openList, ref closedList, start, goal);
                }


                closedList.Add(currentNode);
            }

            return openList;
        }

        public static int GetHeuristic(int x, int y, int x2, int y2)
        {
            int heuristic = (Mathf.Abs(x) + Mathf.Abs(y)) - (Mathf.Abs(x2) + Mathf.Abs(y2));

            return Mathf.Abs(heuristic);
        }

        public static bool CheckClosedListForLowerScore(List<CNode> list, CNode newNode)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].x == newNode.x && list[i].y == newNode.y)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckOpenListForLowerScore(ref List<CNode> list, CNode newNode)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].x == newNode.x && list[i].y == newNode.y)
                {
                    if (newNode.fCost < list[i].fCost)
                    {
                        list.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        public static void PopulateNode(ref CNode node, ref CNode parentNode, ref List<CNode> openList, ref List<CNode> closedList, Vector2 start, Vector2 goal)
        {
            node.heuristic = GetHeuristic(node.x, node.y, Mathf.FloorToInt(goal.x), Mathf.FloorToInt(goal.y));
            node.cost = GetHeuristic(node.x, node.y, Mathf.FloorToInt(start.x), Mathf.FloorToInt(start.y));
            node.parentNode = parentNode;
            node.stepAmount = node.parentNode.stepAmount + node.stepCost;
            node.fCost = node.heuristic + node.cost + node.stepAmount;

            if (!CheckClosedListForLowerScore(closedList, node))
            {
                if (!CheckOpenListForLowerScore(ref openList, node))
                {
                    openList.Add(node);
                }
            }
        }

        public static void CheckLeftRight(Grid terrainMap, ref CNode currentNode, ref CNode parentNode, ref List<CNode> openList, ref List<CNode> closedList, Vector2 start, Vector2 goal)
        {
            CNode node = new CNode();
            if (terrainMap.GetValue(currentNode.x + 1, currentNode.y) != 0)
            {
                node = new CNode();
                node.x = currentNode.x + 1;
                node.y = currentNode.y;
                node.stepCost = 14;
                PopulateNode(ref node, ref parentNode, ref openList, ref closedList, start, goal);



            }

            if (terrainMap.GetValue(currentNode.x - 1, currentNode.y) != 0)
            {
                node = new CNode();
                node.x = currentNode.x - 1;
                node.y = currentNode.y;
                node.stepCost = 14;
                PopulateNode(ref node, ref parentNode, ref openList, ref closedList, start, goal);



            }

        }

        public static void CheckUpDown(Grid terrainMap, ref CNode currentNode, ref CNode parentNode, ref List<CNode> openList, ref List<CNode> closedList, Vector2 start, Vector2 goal)
        {
            CNode node;
            if (terrainMap.GetValue(currentNode.x, currentNode.y + 1) != 0)
            {
                node = new CNode();
                node.x = currentNode.x;
                node.y = currentNode.y + 1;
                node.stepCost = 14;
                PopulateNode(ref node, ref parentNode, ref openList, ref closedList, start, goal);



            }

            if (terrainMap.GetValue(currentNode.x, currentNode.y - 1) != 0)
            {
                node = new CNode();
                node.x = currentNode.x;
                node.y = currentNode.y - 1;
                node.stepCost = 14;
                PopulateNode(ref node, ref parentNode, ref openList, ref closedList, start, goal);



            }

        }

        // Used to set a box cast and display it on the scene
        public static bool BoxCastDebug(float height, float width, Vector3 origin, Vector2 direction, float distance, LayerMask hitMask)
        {
            RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(width, height), 0f, direction, distance, hitMask);

            Color rayColour = Color.red;
            if (hit.collider != null)
            {
                rayColour = Color.green;

            }
            else
            {
                rayColour = Color.red;

            }

           Debug.DrawRay(origin - new Vector3(-(width * 0.5f), (height * 0.5f)), Vector2.down * (distance), rayColour);// right
           Debug.DrawRay(origin - new Vector3(width * 0.5f, (height * 0.5f)), Vector2.down * (distance), rayColour); // left
           Debug.DrawRay(origin - new Vector3(width * 0.5f, (height * 0.5f) + distance), Vector3.right * (width), rayColour);// bottom
           //Debug.DrawRay(origin + new Vector3(width * 0.5f, (height * 0.5f) + distance), Vector3.left * (width), rayColour);// top
           Debug.DrawRay(origin - new Vector3(width * 0.5f, (height * 0.5f)), Vector3.right * (width), rayColour);// top
            // Debug.Log(hit.collider);


            return hit.collider != null;

        }

        //Check it not hitting its self
        // Used to set a box cast and display it on the scene
        public static bool BoxCastDebug(float height, float width, Vector3 origin, Vector2 direction, float distance, LayerMask hitMask, GameObject currentObject)
        {
            RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(width, height), 0f, direction, distance, hitMask);

            Color rayColour = Color.red;
            if (hit.collider != null)
            {
                if(hit.collider.gameObject != currentObject)
                {
                rayColour = Color.green;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                rayColour = Color.red;

            }

            Debug.DrawRay(origin - new Vector3(-(width * 0.5f), (height * 0.5f)), Vector2.down * (distance), rayColour);// right
            Debug.DrawRay(origin - new Vector3(width * 0.5f, (height * 0.5f)), Vector2.down * (distance), rayColour); // left
            Debug.DrawRay(origin - new Vector3(width * 0.5f, (height * 0.5f) + distance), Vector3.right * (width), rayColour);// bottom
            //Debug.DrawRay(origin + new Vector3(width * 0.5f, (height * 0.5f) + distance), Vector3.left * (width), rayColour);// top
            Debug.DrawRay(origin - new Vector3(width * 0.5f, (height * 0.5f)), Vector3.right * (width), rayColour);// top
            // Debug.Log(hit.collider);


            return hit.collider != null;

        }


        //Ray casts to check to see if the maggot shold flip directions
       public static bool maggotRaycasts(Collider2D agentCol, int scale, List<LayerMask> mask)
        {
            RaycastHit2D hit;
            foreach(LayerMask node in mask)
            {
                if(scale == 1)
                {
                 hit = Physics2D.Raycast(agentCol.bounds.center, Vector2.right, agentCol.bounds.extents.x + 0.1f, node);
                }
                else
                {
                   hit = Physics2D.Raycast(agentCol.bounds.center, Vector2.left, agentCol.bounds.extents.x + 0.1f, node);
                }
                    if(hit)
                {
                    //Debug.Log(hit.collider.name);
                    return true;
                }
            }
            return false;
        }

        //Ray casts to check to see if the maggot is trapped
        public static bool maggotTrapped(Collider2D agentCol, List<LayerMask> mask)
        {
            RaycastHit2D hit;
            RaycastHit2D hit2;
            foreach (LayerMask node in mask)
            {                
                    hit = Physics2D.Raycast(agentCol.bounds.center, Vector2.right, agentCol.bounds.extents.x + 0.1f, node);
                if (hit)
                {
                    foreach (LayerMask node2 in mask)
                    {
                        hit2 = Physics2D.Raycast(agentCol.bounds.center, Vector2.left, agentCol.bounds.extents.x + 0.1f, node2);
                        if(hit && hit2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }




    }
}