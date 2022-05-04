using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> edges;

    private void Start()
    {
        edges = new List<Waypoint>();
    }

    private void OnDrawGizmos()
    {
        foreach (Waypoint edge in edges)
        {
            var dir = edge.transform.position - transform.position;
            var normal = Quaternion.Euler(0, -90, 0) * dir;
            var offset = normal.normalized * 0.1f;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + offset, edge.transform.position + offset);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(edge.transform.position - dir.normalized * 0.5f + offset, 0.1f);
        }
    }
}
