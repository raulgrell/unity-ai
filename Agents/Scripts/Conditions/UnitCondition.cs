using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/Unit")]
public class UnitCondition : StateCondition
{
    public override bool Test(FiniteStateMachine fsm)
    {
        return fsm.Agent.Target != null;
    }
}
