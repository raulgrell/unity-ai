using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PursueBehaviour : Steering
{
    public Transform target;
    public float maxPrediction = 2;

    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        var targetVelocity = target.GetComponent<Rigidbody>().velocity;
        var targetSpeed = targetVelocity.magnitude;
        var vec = target.position - transform.position;
        var dist = vec.magnitude;
        var predicted = targetSpeed <= (dist / targetSpeed) ? maxPrediction : dist / targetSpeed;

        var predictedTarget = target.position + targetVelocity * predicted;
        var predictedVec = predictedTarget - transform.position;
        predictedVec.Normalize();
        predictedVec *= sb.maxLinearAccel;
        
        return new SteeringData
        {
            linear = predictedVec,
            angular = 0
        };
    }
}