using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType
{
    Grass,
    Sand,
    Water,
    Max
}


public class GeneticAlgorithm : MonoBehaviour
{
    public int populationSize = 100;
    public int mazeWidth = 20;
    public int mazeHeight = 20;

    [Range(0, 100)] public int popMutationRate = 5;
    [Range(0, 100)] public int geneMutationRate = 50;
    [Range(0, 255)] public int maxGenerations = 100;
    [Range(0, 255)] public int elitismFactor = 5;

    public Material startMaterial;
    public Material endMaterial;

    private static int[] costs = new int[(int) TerrainType.Max];
    private GameObject currentMaze;
    private PathfindingGenetic pathfinding;

    private void GenerateLevelGeometry(Individual individual)
    {
        if (currentMaze)
            DestroyImmediate(currentMaze);

        int centerX = mazeWidth / 2;
        int centerY = mazeHeight / 2;

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(mazeWidth, 1, mazeHeight);
        ground.name = "Maze";
        currentMaze = ground;

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                if (individual.maze[x, y] != 1)
                {
                    if (x == individual.start.x && y == individual.start.y)
                    {
                        GameObject start = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        start.transform.position = new Vector3(x - centerX + 0.5f, 1, y - centerY + 0.5f);
                        start.transform.localScale = new Vector3(1, 1, 1);
                        start.name = "Start";
                        start.transform.SetParent(ground.transform);
                    }

                    if (x == individual.end.x && y == individual.end.y)
                    {
                        GameObject end = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        end.transform.position = new Vector3(x - centerX + 0.5f, 1, y - centerY + 0.5f);
                        end.transform.localScale = new Vector3(1, 1, 1);
                        end.name = "End";
                        end.transform.SetParent(ground.transform);
                    }

                    continue;
                }

                GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(x - centerX + 0.5f, 1, y - centerY + 0.5f);
                wall.transform.localScale = new Vector3(1, 1, 1);
                wall.name = $"Wall_{x}_{y}";

                wall.transform.SetParent(ground.transform);
            }
        }
    }

    private int[,] GenerateIndividual()
    {
        int[,] maze = new int[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                var isBorder = x == 0 || y == 0 || x == mazeWidth - 1 || y == mazeHeight - 1;
                maze[x, y] = isBorder ? 1 : Random.Range(0, 2);
            }
        }

        return maze;
    }

    private Vector2Int GetValidPosition(int[,] maze, Vector2Int position)
    {
        while (maze[position.x, position.y] == 1)
            position = GetRandomPosition();

        return position;
    }

    private List<Individual> GenerateInitialPopulation()
    {
        List<Individual> population = new List<Individual>();
        for (int i = 0; i < populationSize; i++)
        {
            int[,] maze = GenerateIndividual();
            var start = GetValidPosition(maze, GetRandomPosition());
            var end = GetValidPosition(maze, GetRandomPosition());

            population.Add(new Individual(maze, start, end, -1));
        }

        return population;
    }

    private Vector2Int GetRandomPosition()
    {
        return new Vector2Int(Random.Range(1, mazeWidth - 1), Random.Range(1, mazeHeight - 1));
    }

    void EvaluatePopulation(List<Individual> population)
    {
        for (int i = 0; i < populationSize; i++)
        {
            if (population[i].evaluation == -1)
            {
                List<Vector2Int> path = pathfinding.FindPath(population[i].maze, mazeWidth, mazeHeight,
                    population[i].start, population[i].end);

                int fitness = 1;
                if (path.Count > 0)
                    fitness = path.Count;

                population[i].evaluation = fitness;
            }
        }
    }

    private (int parentA, int parentB) Selection(List<Individual> population)
    {
        float totalEvaluation = 0;

        for (int i = 0; i < populationSize; i++)
            totalEvaluation += population[i].evaluation;

        int selectedA = -1;
        int selectedB = -1;
        int selected = Random.Range(1, 101);
        float cumulativeChance = 0;

        for (int i = 0; i < populationSize; i++)
        {
            float chance = population[i].evaluation / totalEvaluation * 100;
            if (selected <= chance + cumulativeChance)
            {
                selectedA = i;
                totalEvaluation -= population[i].evaluation;
                break;
            }

            cumulativeChance += chance;
        }

        selected = Random.Range(1, 101);
        cumulativeChance = 0;

        for (int i = 0; i < populationSize; i++)
        {
            if (i == selectedA)
                continue;

            float chance = population[i].evaluation / totalEvaluation * 100;
            if (selected <= chance + cumulativeChance)
            {
                selectedB = i;
                break;
            }

            cumulativeChance += chance;
        }

        if (selectedA == -1 && selectedB == -1)
            (selectedA, selectedB) = (populationSize - 1, populationSize - 2);
        else if (selectedA == -1)
            selectedA = populationSize - 1;
        else if (selectedB == -1)
            selectedB = populationSize - 1;

        return (selectedA, selectedB);
    }

    private (Individual childA, Individual childB) Crossover(Individual parentA, Individual parentB)
    {
        int crossoverPoint = Random.Range(1, mazeWidth - 1);

        var childA = new Individual(new int[mazeWidth, mazeHeight], Vector2Int.zero, Vector2Int.zero, -1);
        var childB = new Individual(new int[mazeWidth, mazeHeight], Vector2Int.zero, Vector2Int.zero, -1);

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                if (x < crossoverPoint)
                {
                    childA.maze[x, y] = parentA.maze[x, y];
                    childB.maze[x, y] = parentB.maze[x, y];
                }
                else
                {
                    childA.maze[x, y] = parentB.maze[x, y];
                    childB.maze[x, y] = parentA.maze[x, y];
                }
            }
        }

        int startEnd = Random.Range(0, 4);
        if (startEnd == 0)
        {
            childA.start = GetValidPosition(childA.maze, childA.start);
            childA.end = GetValidPosition(childA.maze, childA.end);
            childB.start = GetValidPosition(childB.maze, childB.start);
            childB.end = GetValidPosition(childB.maze, childB.end);
        }
        else if (startEnd == 1)
        {
            childA.start = GetValidPosition(childA.maze, childB.start);
            childA.end = GetValidPosition(childA.maze, childB.end);
            childB.start = GetValidPosition(childB.maze, childA.start);
            childB.end = GetValidPosition(childB.maze, childA.end);
        }
        else if (startEnd == 2)
        {
            childA.start = GetValidPosition(childA.maze, childA.start);
            childA.end = GetValidPosition(childA.maze, childB.end);
            childB.start = GetValidPosition(childB.maze, childB.start);
            childB.end = GetValidPosition(childB.maze, childA.end);
        }
        else if (startEnd == 3)
        {
            childA.start = GetValidPosition(childA.maze, childB.start);
            childA.end = GetValidPosition(childA.maze, childA.end);
            childB.start = GetValidPosition(childB.maze, childA.start);
            childB.end = GetValidPosition(childB.maze, childB.end);
        }

        return (childA, childB);
    }

    private Individual Mutation(Individual individual)
    {
        int r = Random.Range(1, 101);

        if (r <= popMutationRate)
            return individual;

        for (int x = 1; x < mazeWidth - 1; x++)
        {
            for (int y = 1; y < mazeWidth - 1; y++)
            {
                int geneMutation = Random.Range(1, 101);
                if (geneMutation <= geneMutationRate)
                {
                    individual.maze[x, y] = 1 - individual.maze[x, y];
                }
            }
        }

        individual.start = GetValidPosition(individual.maze,
            Random.Range(0, 2) == 0 ? GetRandomPosition() : individual.start);

        individual.end = GetValidPosition(individual.maze,
            Random.Range(0, 2) == 0 ? GetRandomPosition() : individual.end);

        return individual;
    }

    Individual RunGeneticAlgorithm()
    {
        List<Individual> currentPopulation = GenerateInitialPopulation();

        int genCount = 0;

        while (true)
        {
            EvaluatePopulation(currentPopulation);
            currentPopulation.Sort((a, b) => b.evaluation.CompareTo(a.evaluation));

            genCount += 1;
            if (genCount >= maxGenerations)
                break;

            var newPopulation = new List<Individual>(populationSize);
            while (newPopulation.Count < populationSize - elitismFactor)
            {
                var (selA, selB) = Selection(currentPopulation);
                var (childA, childB) = Crossover(currentPopulation[selA], currentPopulation[selB]);
                newPopulation.Add(Mutation(childA));
                newPopulation.Add(Mutation(childB));
            }

            newPopulation.AddRange(currentPopulation.GetRange(0, elitismFactor));

            Debug.Log($"Generation {genCount}: {currentPopulation[0].evaluation}");

            currentPopulation = newPopulation;
        }

        return currentPopulation[0];
    }

    public void BuildLevel()
    {
        pathfinding = new PathfindingGenetic();
        Individual bestMaze = RunGeneticAlgorithm();
        GenerateLevelGeometry(bestMaze);
        
        Debug.Log(bestMaze.evaluation);
    }
}