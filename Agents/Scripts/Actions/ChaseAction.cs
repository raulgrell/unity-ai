using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Chase", fileName = "ChaseAction")]
public class ChaseAction : StateAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        fsm.Agent.GoToTarget();
    }
}