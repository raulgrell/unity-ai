using UnityEngine;
using UnityEngine.AI;

public class ReloadTask : Task
{
    public ReloadTask()
    {
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        if (status == TaskStatus.None)
        {
            Debug.Log("Begin ReloadTask");
            status = TaskStatus.Running;
        }

        unit.Reload();

        status = TaskStatus.Success;
        return status;
    }
}