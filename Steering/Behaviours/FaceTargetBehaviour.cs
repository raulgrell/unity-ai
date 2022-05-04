using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;

public class FaceTargetBehaviour : Steering
{
    public Transform target;

    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        var targetDir = (target.position - transform.position).normalized;
        var angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
        
        return new SteeringData
        {
            linear = Vector3.zero,
            angular = Mathf.LerpAngle(transform.eulerAngles.y, angle, sb.maxAngularAccel * Time.deltaTime)
        };
    }
}