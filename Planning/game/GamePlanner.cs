using System.Collections;
using System.Collections.Generic;
using Planning;
using UnityEngine;

public class GamePlanner : ActionPlanner
{
    public override void DoAction(PlanAction action)
    {
        if (action.name.Equals("GO"))
        {
            agent.destination = GetWaypoint(action.parameters[2]);
            action.status = Status.Running;
        }
        else if (action.name.Equals("GET"))
        {
            Destroy(GameObject.FindGameObjectWithTag(action.parameters[1]));
            action.status = Status.Running;
        }
        else if (action.name.Equals("KILL"))
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
        else if (action.name.Equals("KILL"))
        {
            action.status = Status.Complete;
        }
    }
}
