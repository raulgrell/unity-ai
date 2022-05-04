using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Agent : MonoBehaviour
{
    public AI.Pathfinding pathfinding;
    public float refreshInterval;
    public Transform target;
    public List<Waypoint> path;
    public float speed;

    private Waypoint startNode;
    private Waypoint endNode;
    private int currentIndex;

    IEnumerator Start()
    {
        startNode = FindClosestWaypoint(transform.position);
        endNode = FindClosestWaypoint(target.position);

        var targetPosition = target.position + Vector3.up;
        var endPosition = endNode.transform.position + Vector3.up;
        
        while (true)
        {
            if (targetPosition != target.position)
            {
                targetPosition = target.position;
                endNode = FindClosestWaypoint(targetPosition);
                endPosition = endNode.transform.position;
            }
            
            if (endPosition != endNode.transform.position)
            {
                endPosition = endNode.transform.position;
                path = pathfinding.FindPath(startNode, endNode);
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
            
            yield return new WaitForSeconds(refreshInterval);
        }
    }

    private Waypoint FindClosestWaypoint(Vector3 position)
    {
        var closestDistance = Mathf.Infinity;
        Waypoint result = null;
        
        foreach (Waypoint p in pathfinding.waypoints)
        {
            var dist = Vector3.Distance(position, p.transform.position);
            if (dist > closestDistance)
                continue;
            closestDistance = dist;
            result = p;
        }
        
        return result;
    }

    IEnumerator FollowPath()
    {
        if (path.Count == 0)
            yield break;
        
        currentIndex = 0;

        while (true)
        {
            if (currentIndex == path.Count)
                yield break;

            Vector3 targetPosition = path[currentIndex].transform.position;

            if (transform.position == targetPosition)
                currentIndex += 1;
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
    
    public void OnDrawGizmos()
    {
        if (path == null || path.Count == 0)
            return;

        for (int i = currentIndex; i < path.Count; i++)
        {
            Vector3 lineEnd = path[i].transform.position;
            Vector3 lineStart = (i == currentIndex) ? transform.position : path[i - 1].transform.position;
            Gizmos.color = (i == currentIndex) ? Color.yellow : Color.blue; 
            
            Gizmos.DrawSphere(lineEnd, 0.2f);
            Gizmos.DrawLine(lineStart, lineEnd);
        }
    }
}
