﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallel : Task
{
    public override TaskStatus Run(UnitController unit, World wm)
    {
        int successCount = 0;
        foreach (Task task in children)
        {
            if (task.status != TaskStatus.Success)
            {
                TaskStatus childrenStatus = task.Run(unit, wm);
                if (childrenStatus == TaskStatus.Failure)
                {
                    status = TaskStatus.Failure;
                    return status;
                }
                
                if (childrenStatus == TaskStatus.Success)
                    successCount++;
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