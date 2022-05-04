using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EvadeBehaviour : Steering
{
    public Transform target;
    public float maxPrediction;

    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        var targetPosition = target.position;
        var targetVelocity = target.GetComponent<Rigidbody>().velocity;
        var targetSpeed = targetVelocity.magnitude;
        var targetVec = targetPosition - transform.position;
        var dist = targetVec.magnitude;
        var predictedTime = targetSpeed <= (dist / targetSpeed) ? maxPrediction : dist / targetSpeed;
        var predictedTarget = targetPosition + targetVelocity * predictedTime;
        var predictedVec =  (transform.position - predictedTarget).normalized * sb.maxLinearAccel;
        
        return new SteeringData
        {
            linear = predictedVec,
            angular = 0
        };
    }
}