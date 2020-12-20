using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost; } }

    public Node cameFromNode;

    public bool isWall = false;
    public int x
    {
        get { return (int)transform.position.x; }
    }
    public int y
    {
        get { return (int)transform.position.y; }
    }

    public bool IsWall
    {
        get { return isWall; }
        set { isWall = value; }
    }
}
