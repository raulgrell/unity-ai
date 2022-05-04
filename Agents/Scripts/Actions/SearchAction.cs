using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Search", fileName = "SearchAction")]
public class SearchAction : StateAction
{
    public float energyCost;
    
    public override void Act(FiniteStateMachine fsm)
    {
        if (fsm.Agent.isAtDestination())
        {
            fsm.Agent.GoToNextWaypoint();
        }
        fsm.Agent.Energy -= energyCost * Time.deltaTime;
    }
}