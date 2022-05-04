using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/Can See")]
public class VisibilityCondition : StateCondition
{
    [SerializeField] private bool negation;
    [SerializeField] private float viewAngle;
    [SerializeField] private float viewDistance;
    
    public override bool Test(FiniteStateMachine fsm)
    {
        var agent = fsm.transform;
        var agentPosition = agent.position;
        
        var target = fsm.Agent.Target;
        var targetPosition = target.position;
        
        var direction = (targetPosition - agentPosition).normalized;
        var angle = Vector3.Angle(direction, agent.forward);
        var distance = Vector3.Distance(agentPosition, targetPosition);

        if (angle < viewAngle && distance < viewDistance)
        {
            return !negation;
        }
        
        return negation;
    }
}
