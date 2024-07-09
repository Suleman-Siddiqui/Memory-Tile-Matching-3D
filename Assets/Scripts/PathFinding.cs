using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding pFInstance;

    private void Awake()
    {
        pFInstance = this;
    }

    public List<Vector3> FindPath(Vector3 start, Vector3 goal)
    {
        List<Vector3> path = new List<Vector3>();

        // Example A* implementation for hexagonal grid
        // Replace with your actual A* algorithm tailored for hex grid

        Vector3 current = start;
        path.Add(current);

        while (current != goal)
        {
            // Example: move toward goal in a straight line
            current = Vector3.MoveTowards(current, goal, 1f); // Adjust speed and step size
            path.Add(current);
        }

        return path;
    }
}




