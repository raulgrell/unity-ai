using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;

public class AlignmentBehaviour : Steering
{
    private Transform[] targets;
    [SerializeField] private float alignDistance = 8f;

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
        steering.linear = Vector3.zero;
        int count = 0;
        foreach (Transform target in targets)
        {
            Vector3 targetDir = target.position - transform.position;
            if (targetDir.magnitude < alignDistance)
            {
                steering.linear += target.GetComponent<Rigidbody>().velocity;
                count++;
            }
        }

        if (count <= 0) return steering;
        
        steering.linear = steering.linear / count;
        if (steering.linear.magnitude > sb.maxLinearAccel)
        {
            steering.linear = steering.linear.normalized * sb.maxAngularAccel;
        }

        return steering;
    }
}