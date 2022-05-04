using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class PathfindingGenetic
{
    float Heuristic(Vector2Int w1, Vector2Int w2)
    {
        return Vector2Int.Distance(w1, w2);
    }

    List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current, Vector2Int start)
    {
        List<Vector2Int> finalPath = new List<Vector2Int>();
        finalPath.Add(current);

        while (current != start)
        {
            current = cameFrom[current];
            finalPath.Add(current);
        }
        
        finalPath.Reverse();
        return finalPath;
    }

    private List<Vector2Int> GetNeighbours(int[,] maze, int mazeWidth, int mazeHeight, Vector2Int node )
    {
        List<Vector2Int> neighbour = new List<Vector2Int>();
        if (node.x + 1 < mazeWidth && maze[node.x + 1, node.y] == 0)
            neighbour.Add(new Vector2Int(node.x + 1, node.y));

        if (node.x - 1 > 0 && maze[node.x - 1, node.y] == 0)
            neighbour.Add(new Vector2Int(node.x - 1, node.y));

        if (node.y + 1 < mazeHeight && maze[node.x, node.y + 1] == 0)
            neighbour.Add(new Vector2Int(node.x, node.y + 1));

        if (node.y - 1 > 0 && maze[node.x, node.y - 1] == 0)
            neighbour.Add(new Vector2Int(node.x, node.y - 1));

        return neighbour;
    }

    public List<Vector2Int> FindPath(int[,] maze, int mazeWidth, int mazeHeight, Vector2Int start, Vector2Int goal)
    {
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, float>();
        
        var closedSet = new List<Vector2Int>();
        var openSet = new SimplePriorityQueue<Vector2Int>();
        openSet.Enqueue(start, Heuristic(start, goal));

        for(int x = 0; x < mazeWidth; x++)
        {
            for(int y = 0; y < mazeHeight; y++)
                gScore.Add(new Vector2Int(x, y), Mathf.Infinity);
        }
        
        gScore[start] = 0;

        while (openSet.Count > 0)
        {
            Vector2Int current = openSet.Dequeue();

            if (current == goal)
                return ReconstructPath(cameFrom, current, start);

            closedSet.Add(current);
            List<Vector2Int> neighbours = GetNeighbours(maze, mazeWidth, mazeHeight, current);
            foreach (Vector2Int neighbour in neighbours)
            {
                if (closedSet.Contains(neighbour))
                    continue;

                if (!openSet.Contains(neighbour))
                    openSet.Enqueue(neighbour, gScore[neighbour] + Heuristic(neighbour, goal));

                float tentative_score = gScore[current] + Heuristic(current, neighbour);

                if (tentative_score >= gScore[neighbour])
                    continue;

                cameFrom[neighbour] = current;
                gScore[neighbour] = tentative_score;
                openSet.UpdatePriority(neighbour, gScore[neighbour] + Heuristic(neighbour, goal));
            }
        }
        
        return new List<Vector2Int>();
    }
}