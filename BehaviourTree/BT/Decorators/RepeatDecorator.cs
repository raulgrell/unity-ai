using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatDecorator : Decorator
{
    public override TaskStatus Run(UnitController unit, World wm)
    {
        TaskStatus childStatus = Child.Run(unit, wm);
        if (childStatus == TaskStatus.Success)
        {
            ResetTree(this);
            return TaskStatus.Running;
        }

        return TaskStatus.Running;
    }

    private void ResetTree(Task child)
    {
        child.status = TaskStatus.None;
        foreach (Task task in child.Children)
        {
            ResetTree(task);
        }
    }
}
