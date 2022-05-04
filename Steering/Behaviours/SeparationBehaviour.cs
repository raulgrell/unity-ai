using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationBehaviour : Steering
{
    private Transform[] targets;
    [SerializeField] private float threshold = 2f;
    [SerializeField] private float decayCoefficient = -25f;

    void Start()
    {
        SteeringBehaviour[] agents = FindObjectsOfType<SteeringBehaviour>();
        targets = new Transform[agents.Length - 1];
        int count = 0;
        foreach (SteeringBehaviour agent in agents)
        {
            if (agent.gameObject == gameObject) continue;
            targets[count] = agent.transform;
            count++;
        }
    }

    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        SteeringData steering = new SteeringData();
        foreach (Transform target in targets)
        {
            Vector3 direction = target.transform.position - transform.position;
            float distance = direction.magnitude;
            if (!(distance < threshold)) continue;
            float strength = Mathf.Min(decayCoefficient / (distance * distance), sb.maxLinearAccel);
            direction.Normalize();
            steering.linear += strength * direction;
        }

        return steering;
    }
}