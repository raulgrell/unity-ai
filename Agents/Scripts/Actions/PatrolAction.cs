using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Patrol", fileName = "PatrolAction")]

public class PatrolAction : StateAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        if (fsm.Agent.isAtDestination())
        {
            fsm.Agent.GoToNextWaypoint();
        }
    }
}
