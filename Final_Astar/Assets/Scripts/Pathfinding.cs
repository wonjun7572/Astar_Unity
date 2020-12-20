using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Grid grid;
    public List<Node> openList;
    public List<Node> closeedList;

    public List<Node> ASTAR_PathFind(float startx, float starty, float endx, float endy)
    {
        int sX = Mathf.RoundToInt(startx);
        int sY = Mathf.RoundToInt(starty);
        int eX = Mathf.RoundToInt(endx);
        int eY = Mathf.RoundToInt(endy);

        Node startNode = grid.nodes[sX, sY];
        Node endNode = grid.nodes[eX, eY];

        openList = new List<Node>() { startNode };
        closeedList = new List<Node>();

        if (startNode.IsWall)
            return null;

        for(int x = 0; x<grid.gridSize.x; ++x)
        {
            for(int y = 0; y < grid.gridSize.y; ++y)
            {
                Node pathNode = grid.nodes[x, y];
                pathNode.gCost = int.MaxValue;
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);

        while(openList.Count > 0) {
            Node currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(currentNode);
            }

            openList.Remove(currentNode);
            closeedList.Add(currentNode);


            foreach(Node neighborNode in GetNeighborList(currentNode))
            {
                if (neighborNode.IsWall) continue;
                if (closeedList.Contains(neighborNode)) continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighborNode);
                if( tentativeGCost < neighborNode.gCost)
                {
                    neighborNode.cameFromNode = currentNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistanceCost(neighborNode, endNode);

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        return null;
    }

    List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    Node GetLowestFCostNode(List<Node> openList)
    {
        Node lowestFCostNode = openList[0];
        for(int i =0; i<openList.Count; ++i)
        {
            if(openList[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = openList[i];
            }
        }
        return lowestFCostNode;
    }

    int CalculateDistanceCost(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }


    List<Node> GetNeighborList(Node currentNode)
    {
        List<Node> neighborNodes = new List<Node>();
        if(currentNode.x - 1>= 0)
        {
            neighborNodes.Add(grid.nodes[currentNode.x - 1, currentNode.y]);
            if (currentNode.y - 1>= 0) neighborNodes.Add(grid.nodes[currentNode.x - 1 , currentNode.y - 1]);
            if (currentNode.y + 1 < grid.gridSize.y) neighborNodes.Add(grid.nodes[currentNode.x - 1, currentNode.y + 1]);
        }

        if(currentNode.x + 1 < grid.gridSize.x)
        {
            neighborNodes.Add(grid.nodes[currentNode.x + 1, currentNode.y]);
            if (currentNode.y - 1 >= 0) neighborNodes.Add(grid.nodes[currentNode.x + 1, currentNode.y - 1]);
            if (currentNode.y + 1 < grid.gridSize.y) neighborNodes.Add(grid.nodes[currentNode.x + 1, currentNode.y + 1]);
        }

        if (currentNode.y + 1 < grid.gridSize.y) neighborNodes.Add(grid.nodes[currentNode.x, currentNode.y + 1]);
        if (currentNode.y - 1 >= 0) neighborNodes.Add(grid.nodes[currentNode.x, currentNode.y - 1]);

        return neighborNodes;
    }
}
