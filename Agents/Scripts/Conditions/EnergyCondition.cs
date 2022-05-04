using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyThreshold
{
    Minimum,
    Maximum
}

[CreateAssetMenu(menuName = "FSM/Conditions/Energy")]
public class EnergyCondition : StateCondition
{
    public EnergyThreshold type;
    public float value;
    
    public override bool Test(FiniteStateMachine fsm)
    {
        switch (type)
        {
            case EnergyThreshold.Minimum: return fsm.Agent.Energy > value;
            case EnergyThreshold.Maximum: return fsm.Agent.Energy < value;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
