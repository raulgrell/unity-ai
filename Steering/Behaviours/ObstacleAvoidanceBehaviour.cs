using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehaviour : Steering
{
    [SerializeField] private float avoidDistance = 1f;
    [SerializeField] private float lookDistance = 2f;
    [SerializeField] private float viewAngle = 45f;
    [SerializeField] private LayerMask obstacleLayer;
    
    private Vector3 OrientationToVector(float orientation)
    {
        return new Vector3(Mathf.Sin(orientation), 0, Mathf.Cos(orientation));
    }

    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        SteeringData steering = new SteeringData();
        
        var body = GetComponent<Rigidbody>();
        var velocity = body.velocity;
        
        Vector3[] rayVector = new Vector3[3];
        
        float rayDir = Mathf.Atan2(velocity.x, velocity.z);
        rayVector[0] = velocity.normalized * lookDistance;
        
        float rightRayDir = rayDir + viewAngle * Mathf.Deg2Rad;
        rayVector[1] = OrientationToVector(rightRayDir) * lookDistance;
        
        float leftRayDir = rayDir - viewAngle * Mathf.Deg2Rad;
        rayVector[2] = OrientationToVector(leftRayDir) * lookDistance;
        
        foreach (Vector3 t in rayVector)
        {
            if (!Physics.Raycast(transform.position, t, out RaycastHit hit, lookDistance, obstacleLayer.value)) continue;
            Vector3 target = hit.point + hit.normal * avoidDistance;
            steering.linear = target - transform.position;
            steering.linear.Normalize();
            steering.linear *= sb.maxLinearAccel;
            break;
        }

        return steering;
    }
}