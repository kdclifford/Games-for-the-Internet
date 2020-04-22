using System;
using System.Collections;
using System.Collections.Generic;
using Functions.Utils;
using UnityEngine;


public class CNodeList
{
    public CNode Node;
    public CNode parentNode;   
}

public class CNode
{
    public int x;
    public int y;
    public int score = 0;
    public int cost = 1;
}
