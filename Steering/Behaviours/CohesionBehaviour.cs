using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehaviour : Steering
{
    private Transform[] targets;
    [SerializeField] private float viewAngle = 60f;

    void Start()
    {
        SteeringBehaviour[] agents = FindObjectsOfType<SteeringBehaviour>();
        targets = new Transform[agents.Length - 1];
        int count = 0;
        foreach (SteeringBehaviour agent in agents)
        {
            if (agent.gameObject != gameObject)
            {
                targets[count] = agent.transform;
                count++;
            }
        }
    }

    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        SteeringData steering = new SteeringData();
        Vector3 centerOfMass = Vector3.zero;
        int count = 0;
        foreach (Transform target in targets)
        {
            Vector3 targetDir = target.position - transform.position;
            if (Vector3.Angle(targetDir, transform.forward) >= viewAngle) continue;
            centerOfMass += target.position;
            count++;
        }

        if (count == 0) return steering;
        
        centerOfMass = centerOfMass / count;
        steering.linear = centerOfMass - transform.position;
        steering.linear.Normalize();
        steering.linear *= sb.maxLinearAccel;

        return steering;
    }
}