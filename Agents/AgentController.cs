using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AgentState
{
    Patrol = 0,
    Chase = 1,
    Attacking = 2
}

public class AgentController : MonoBehaviour
{
    public List<Transform> goals;
    public Transform player;
    public int currentGoalIndex;
    private AgentState state;
    private NavMeshAgent agent;
    private int currentColor;
    private MeshRenderer mesh;
    private BoxCollider box;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        state = AgentState.Patrol;

        if (goals.Count == 0)
        {
            var sceneGoals = FindObjectsOfType<Goal>();
            foreach (Goal goal in sceneGoals)
            {
                goals.Add(goal.transform);
            }
        }

        if (goals[0])
            agent.destination = goals[0].position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        switch (state)
        {
            case AgentState.Patrol:
                mesh.material.color = Color.blue;
                agent.destination = goals[currentGoalIndex].position;
                if (Mathf.Approximately(agent.remainingDistance, 0))
                {
                    currentGoalIndex += 1;
                    if (currentGoalIndex >= goals.Count)
                        currentGoalIndex = 0;
                }
                if (distanceToPlayer < 5)
                {
                    state = AgentState.Chase;
                }
                break;
            case AgentState.Chase:
                mesh.material.color = Color.yellow;
                agent.destination = player.position;
                if (distanceToPlayer < 1)
                {
                    state = AgentState.Attacking;
                }
                else if (distanceToPlayer > 5)
                {
                    state = AgentState.Patrol;
                }
                break;
            case AgentState.Attacking:
                mesh.material.color = Color.red;
                agent.destination = transform.position;
                if (distanceToPlayer > 2)
                {
                    state = AgentState.Chase;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
