﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform recovery;
    [SerializeField] public Waypoint[] waypoints;

    private int waypointIndex;
    private float energy;
    private NavMeshAgent agent;

    public Transform Target => target;
    public Transform Recovery => recovery;
    
    public float Energy
    {
        get => energy;
        set => energy = value;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;

        waypointIndex = 0;
        energy = 100;
    }

    public void GoToClosestWaypoint(Vector3 position)
    {
        var closestDistance = Mathf.Infinity;
        for (var index = 0; index < waypoints.Length; index++)
        {
            Waypoint p = waypoints[index];
            var dist = Vector3.Distance(position, p.transform.position);
            if (dist > closestDistance)
                continue;
            closestDistance = dist;
            waypointIndex = index;
        }
    }

    public void GoToNextWaypoint()
    {
        agent.destination = waypoints[waypointIndex].transform.position;
        waypointIndex += 1;

        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    public void GoToTarget()
    {
        agent.destination = target.position;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    public bool isAtDestination()
    {
        return !agent.pathPending
               && agent.remainingDistance <= agent.stoppingDistance
               && (!agent.hasPath || agent.velocity.sqrMagnitude == 0);
    }

    public void GoToRecovery()
    {
        agent.destination = recovery.position;
    }
}