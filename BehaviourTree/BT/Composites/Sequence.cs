using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Task
{
    public override TaskStatus Run(UnitController unit, World wm)
    {
        int successCount = 0;
        foreach (Task task in children)
        {
            if (task.status != TaskStatus.Success)
            {
                TaskStatus childStatus = task.Run(unit, wm);
                if (childStatus == TaskStatus.Failure)
                {
                    status = TaskStatus.Failure;
                    return status;
                }
                
                if (childStatus == TaskStatus.Success)
                    successCount++;
                else
                    break;
            }
            else
            {
                successCount++;
            }
        }

        status = successCount == children.Count ? TaskStatus.Success : TaskStatus.Running;
        return status;
    }
}