using UnityEngine;

public class FollowPathBehaviour : Steering
{
    [SerializeField] private PathLine path;
    [SerializeField] private float pathOffset = 0.8f;
    [SerializeField] private float predictionOffset = 0.1f;
    [SerializeField] private bool loop;
    private float currentParam = 0;
    
    
    public override SteeringData GetSteering(SteeringBehaviour sb)
    {
        SteeringData steering = new SteeringData();
        Vector3 currentPos = transform.position;
        Vector3 targetPosition = Vector3.zero;
        
        if (path.nodes.Length == 1)
        {
            targetPosition = path.nodes[0];
        }
        else if (path.nodes.Length > 1)
        {
            if (predictionOffset <= 0)
                currentParam = path.GetParam(currentPos);
            else
            {
                var velocity = GetComponent<Rigidbody>().velocity;
                currentPos = currentPos + velocity * predictionOffset;
                currentParam = path.GetParam(currentPos);
            }
            float targetParam = currentParam + pathOffset;
            targetPosition = path.GetPosition(targetParam, loop);
        }
        
        steering.linear = targetPosition - currentPos;
        steering.linear.Normalize();
        steering.linear *= sb.maxLinearAccel;

        return steering;
    }

    private void OnDrawGizmos()
    {
        path.Draw();
    }
}