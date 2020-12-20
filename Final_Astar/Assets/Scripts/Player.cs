using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Pathfinding pathfinding;

    List<Node> path;

    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);
            if(hit.transform.tag == "Node")
            {
                if (!hit.transform.GetComponent<Node>().IsWall)
                {
                    path = pathfinding.ASTAR_PathFind(transform.position.x, transform.position.y, hit.transform.position.x, hit.transform.position.y);
                }
            }
        }


        if (path != null && path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[0].transform.position, 5f * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[0].transform.position) < 0.1f)
            {
                transform.position = path[0].transform.position;
                path.Remove(path[0]);
            }
        }
    }
}
