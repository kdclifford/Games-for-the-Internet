using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;

public class GridSettings : MonoBehaviour {

    private Grid grid;
    public Vector2 gridPosition;
    public float gridSize;
    public int gridWidth;
    public int gridHeight;
    public LayerMask floorMask;
    public float agentHeight;
    public Color gridColour;

    private void Start() {
        grid = new Grid(gridWidth, gridHeight, gridSize, gridPosition);

       grid = KylesFunctions.GridValues(grid, floorMask, agentHeight, gridColour);



    }

    public Grid GetGrid()
    {
        grid = KylesFunctions.GridValues(grid, floorMask, agentHeight, gridColour);
        return grid;
    }

   

}
