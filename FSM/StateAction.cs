using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class StateAction : ScriptableObject
{
    public abstract void Act(FiniteStateMachine fsm);
}
