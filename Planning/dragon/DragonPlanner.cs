using System.Collections;
using System.Collections.Generic;
using Planning;
using UnityEngine;

public class DragonPlanner : ActionPlanner
{
    public override void DoAction(PlanAction action)
    {
        if (action.name.Equals("GO"))
        {
            agent.destination = GetWaypoint(action.parameters[1]);
            action.status = Status.Running;
        }
        else if (action.name.Equals("GET"))
        {
            Destroy(GameObject.FindGameObjectWithTag(action.parameters[0]));
            action.status = Status.Running;
        }
        else if (action.name.Equals("LOOT"))
        {
            Destroy(GameObject.FindGameObjectWithTag(action.parameters[0]));
            action.status = Status.Running;
        }
        else if (action.name.StartsWith("KILL"))
        {
            Destroy(GameObject.FindGameObjectWithTag(action.parameters[1]));
            action.status = Status.Running;
        }
    }

    public override void CheckAction(PlanAction action)
    {
        if (action.name.Equals("GO"))
        {
            if (isAtDestination())
                action.status = Status.Complete;
        }
        else if (action.name.Equals("GET"))
        {
            action.status = Status.Complete;
        }
        else if (action.name.Equals("LOOT"))
        {
            action.status = Status.Complete;
        }
        else if (action.name.StartsWith("KILL"))
        {
            action.status = Status.Complete;
        }
    }
}
