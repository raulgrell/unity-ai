using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/Target")]
public class TargetCondition: StateCondition
{
    public override bool Test(FiniteStateMachine fsm)
    {
        return fsm.Agent.Target != null;
    }
}
