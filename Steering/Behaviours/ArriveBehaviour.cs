using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;

public class ArriveBehaviour : Steering
{
    public Transform target;
    public float stopRadius = 1.1f;
    public float slowRadius = 5f;
    
    static float Map(float v, float a1, float a2, float b1, float b2) => b1 + (v - a1) * (b2 - b1) / (a2 - a1);

    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        var result = new SteeringData();
        var vec = target.position - transform.position;
        var dist = vec.magnitude;
        var dir = vec.normalized;

        if (dist < stopRadius)
        {
            sb.Body.velocity = Vector3.zero;
            return new SteeringData {linear = Vector3.zero, angular = 0};
        }

        float targetSpeed = (slowRadius < dist)
            ? sb.maxLinearAccel * Map(dist, 1.1f, 5, 0, 1)
            : sb.maxLinearAccel;

        var targetVelocity = dir * targetSpeed;
        result.linear = targetVelocity - sb.Body.velocity;

        if (result.linear.magnitude > sb.maxLinearAccel)
        {
            result.linear.Normalize();
            result.linear *= sb.maxLinearAccel;
        }

        return result;
    }
}