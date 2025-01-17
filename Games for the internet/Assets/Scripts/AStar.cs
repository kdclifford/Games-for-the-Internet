﻿using System;
using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;


public class CNodePosition
{
    public int x;
    public int y;
}

public class CNode
{
    public int x { get; set; }
    public int y { get; set; }
   // public  CNodePosition parentPos;

    public int heuristic { get; set; }
    public int cost { get; set; } //Cost to move there
    public int stepAmount { get; set; } //Cost to move there
    public int stepCost { get; set; } //Cost to move there
    public int fCost { get; set; } //Finale Cost. e.g. cost + heuristic
    public CNode parentNode { get; set; }

   // public int parentNodeX { get; set; }
   // public int parentNodeY { get; set; }

    public CNode()
    {
        heuristic = 0;
        cost = 0;
        stepAmount = 0;
        stepCost = 10;
        fCost = 0;
        //parentPos = new CNodePosition();
    }

}
