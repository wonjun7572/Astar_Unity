using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject nodeObject;
    public Vector2 gridSize;
    public LayerMask wallLayerMask;

    public Node[,] nodes;

    private void Awake()
    {
        nodes = new Node[(int)gridSize.x + 1, (int)gridSize.y + 1];
        CreateGrid();
    }

    void CreateGrid()
    {
       for(int x = 0; x <= gridSize.x; ++x)
       {
            for (int y = 0; y <= gridSize.y; ++y)
            {
                nodes[x, y] = Instantiate(nodeObject, new Vector3(x, y), Quaternion.identity, transform).GetComponent<Node>();

                if (Physics2D.OverlapCircle(new Vector2(x, y), 0.1f, wallLayerMask))
                {
                    nodes[x, y].IsWall = true;
                }

            }
        }
    }
}
