using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SteeringBehaviour : MonoBehaviour
{
    public float maxLinearAccel = 10;
    public float maxAngularAccel = 5;
    public float drag = 1;

    private Rigidbody body;
    private Steering[] steerings;

    public Rigidbody Body => body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.drag = drag;
        
        steerings = GetComponents<Steering>();
    }

    private void FixedUpdate()
    {
        var linear = Vector3.zero;
        var angular = 0f;
        
        foreach (Steering steering in steerings)
        {
            var data = steering.GetSteering(this);
            linear += data.linear * steering.Weight;
            angular += data.angular * steering.Weight;
        }

        if (linear.magnitude > maxLinearAccel)
        {
            linear.Normalize();
            linear *= maxLinearAccel;
        }
        
        body.AddForce(linear);

        if (angular != 0)
        {
            body.rotation = Quaternion.Euler(0, angular, 0);
        }
    }
}
