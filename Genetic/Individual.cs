using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual
{
    public readonly int[,] maze;
    public Vector2Int start;
    public Vector2Int end;
    public float evaluation;

    public Individual(int[,] nmaze, Vector2Int nstart, Vector2Int nend, float nevaluation)
    {
        maze = nmaze;
        start = nstart;
        end = nend;
        evaluation = nevaluation;
    }
}
