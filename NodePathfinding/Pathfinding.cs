using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Priority_Queue;
using UnityEngine;

namespace AI
{
    public class Pathfinding : MonoBehaviour
    {
        public Waypoint[] waypoints;
        public LayerMask waypointLayers;

        public List<Waypoint> FindPath(Waypoint start, Waypoint goal)
        {
            var closedSet = new HashSet<Waypoint>();

            var openSet = new SimplePriorityQueue<Waypoint>();
            openSet.Enqueue(start, Heuristic(start, goal));

            var parentMap = new Dictionary<Waypoint, Waypoint>();
            var gCostMap = new Dictionary<Waypoint, float>();

            foreach (Waypoint t in waypoints)
                gCostMap.Add(t, Mathf.Infinity);
            
            gCostMap[start] = 0;

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();
                
                if (current == goal)
                    return MakePath(parentMap, current, start);
                
                closedSet.Add(current);

                foreach (Waypoint neighbour in current.edges)
                {
                    if (closedSet.Contains(neighbour))
                        continue;

                    if (!openSet.Contains(neighbour))
                        openSet.Enqueue(neighbour, gCostMap[neighbour] + Heuristic(neighbour, goal));

                    var newCost = gCostMap[current] + Heuristic(current, neighbour);
                    if (newCost >= gCostMap[neighbour])
                        continue;

                    parentMap[neighbour] = current;
                    gCostMap[neighbour] = newCost;
                    openSet.UpdatePriority(neighbour, gCostMap[neighbour] + Heuristic(neighbour, goal));
                }
            }
            
            return new List<Waypoint>();
        }

        private List<Waypoint> MakePath(Dictionary<Waypoint,Waypoint> parentMap, Waypoint current, Waypoint start)
        {
            var path = new List<Waypoint> {current};
            
            while (current != start)
            {
                current = parentMap[current];
                path.Add(current);
            }
            
            path.Reverse();
            return path;
        }

        float Heuristic(Waypoint start, Waypoint goal)
        {
            return Vector3.Distance(start.transform.position, goal.transform.position);
        }

        [Button("Generate Edges")]
        private void GenerateEdges()
        {
            var vertices = GetComponentsInChildren<Waypoint>();
            
            foreach (Waypoint waypoint in vertices)
            {
                waypoint.edges.Clear();
            }
            
            for (int i = 0; i < vertices.Length; i++)
            {
                for (int j = 0; j < vertices.Length; j++)
                {
                    if (i == j) continue;
                    var dir = vertices[j].transform.position - vertices[i].transform.position;
                    var ray = new Ray(vertices[i].transform.position, dir);
                    if (Physics.Raycast(ray, 100, waypointLayers))
                    {
                        waypoints[i].edges.Add(waypoints[j]);
                        waypoints[j].edges.Add(waypoints[i]);
                    }
                }
            }
        }
    }
}
