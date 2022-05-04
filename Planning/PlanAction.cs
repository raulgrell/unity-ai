using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Ready,
    Running,
    Complete
}

public class PlanAction
{
    public readonly string name;
    public readonly List<string> parameters = new List<string>();
    public Status status;

    public PlanAction(string action)
    {
        var command = action.Substring(1, action.Length - 2);
        var terms = command.Split(' ');
        
        name = terms[0];
        for (int i = 1; i < terms.Length; i++)
        {
            parameters.Add(terms[i]);
        }

        status = Status.Ready;
    }
}
