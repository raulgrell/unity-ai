using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GeneticAlgorithm))]
public class GeneticAlgorithmEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var genetic = (GeneticAlgorithm)target;
        
        DrawDefaultInspector();

        if (GUILayout.Button("Build level"))
        {
            genetic.BuildLevel();
        }
    }
}