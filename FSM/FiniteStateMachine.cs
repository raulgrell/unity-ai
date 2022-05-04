using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StateController))]
public class FiniteStateMachine : MonoBehaviour
{
    public State initialState;
    
    private StateController agent;
    private State currentState;

    public StateController Agent => agent;
    
    private void Start()
    {
        currentState = initialState;
        agent = GetComponent<StateController>();
    }

    private void Update()
    {
        StateTransition triggered = null;
       
        foreach (var transition in currentState.Transitions)
        {
            if (transition.IsTriggered(this))
            {
                triggered = transition;
                break;
            }
        }

        var actions = new List<StateAction>();
        actions.AddRange(currentState.Actions);
        
        if (triggered)
        {
            Debug.Log($"{currentState} -> {triggered}");
            if (currentState.ExitAction)
                actions.Add(currentState.ExitAction);

            if (triggered.Target.EntryAction)
                actions.Add(triggered.Target.EntryAction);
            
            if (triggered.Action)
                actions.Add(triggered.Action);
            
            DoActions(actions);
            currentState = triggered.Target;
        }
        else
        {
            DoActions(actions);
        }
    }

    private void DoActions(List<StateAction> actions)
    {
        foreach (StateAction action in actions)
        {
            action.Act(this);
        }
    }
}