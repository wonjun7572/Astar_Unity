using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;

    Pathfinding pathfinding;

    List<Node> path;

    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        StartCoroutine(FindPlayer());
    }


    private void Update()
    {
        if (path != null && path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[0].transform.position, 2f * Time.deltaTime);

            if (Vector2.Distance(transform.position, path[0].transform.position) < 0.5f)
            {
                path.Remove(path[0]);
            }

            for (int i = 0; i < path.Count - 1; ++i)
            {
                Debug.DrawLine(path[i].transform.position, path[i + 1].transform.position, Color.red);
            }
        }
    }

    IEnumerator FindPlayer()
    {
        while (true)
        {
            List<Node> temp = pathfinding.ASTAR_PathFind(transform.position.x, transform.position.y, target.transform.position.x, target.transform.position.y);

            if (temp != null)
                path = temp;

            yield return new WaitForSeconds(1f);
        }
    }
}
