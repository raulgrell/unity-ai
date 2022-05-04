using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntilFailDecorator : Decorator
{
    public override TaskStatus Run(UnitController unit, World wm)
    {
        if (status == TaskStatus.None) status = TaskStatus.Running;
        if (Child.Run(unit, wm) == TaskStatus.Failure) status = TaskStatus.Success;
        return status;
    }
}