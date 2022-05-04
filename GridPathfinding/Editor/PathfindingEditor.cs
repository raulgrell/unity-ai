using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pathfinding))]
public class PathfindingEditor : Editor
{
    Pathfinding pathfinding;
    Grid Grid => pathfinding.grid;

    void OnEnable()
    {
        pathfinding = target as Pathfinding;
        if (pathfinding.grid == null)
            pathfinding.Init();
    }

    void OnSceneGUI()
    {
        
    }

    void HandleInput()
    {

    }

    void Draw()
    {

    }
}
