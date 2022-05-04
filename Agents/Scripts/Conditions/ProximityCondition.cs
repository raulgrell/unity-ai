using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/Proximity")]
public class ProximityCondition: StateCondition
{
    [SerializeField] private bool negation;
    [SerializeField] private bool viewAngle;
    [SerializeField] private bool viewDistance;
    
    public override bool Test(FiniteStateMachine fsm)
    {
        throw new System.NotImplementedException();
    }
}
