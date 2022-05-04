using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FleeBehaviour : Steering
{
    public Transform target;
    
    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        var dir = (transform.position - target.position).normalized;
        return new SteeringData { linear = dir * sb.maxLinearAccel, angular = 0 };
    }
}
