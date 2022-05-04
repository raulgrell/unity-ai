using UnityEngine;
using UnityEngine.AI;

public class MoveTask : Task
{
    private readonly string destination;

    public MoveTask(string dest)
    {
        destination = dest;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        if (status == TaskStatus.None)
        {
            Debug.Log("Begin MoveTask: " + destination);
            var dest = wm.GetWaypoint(destination).position;
            unit.Agent.destination = dest;
            status = TaskStatus.Running;
            return status;
        }

        if (!unit.isAtDestination()) return status;
        
        Debug.Log("End MoveTask: " + destination);            
        status = TaskStatus.Success;
        return status;

    }
}