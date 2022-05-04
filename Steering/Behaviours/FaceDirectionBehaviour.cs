using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;

public class FaceDirectionBehaviour : Steering
{
    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        var velocity = sb.Body.velocity;
        if (velocity.magnitude == 0) return new SteeringData();
            
        var angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        
        return new SteeringData
        {
            linear = Vector3.zero,
            angular = Mathf.LerpAngle(transform.eulerAngles.y, angle, sb.maxAngularAccel * Time.deltaTime)
        };
    }
}