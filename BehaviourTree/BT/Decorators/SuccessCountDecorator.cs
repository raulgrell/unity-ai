using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessCountDecorator : Decorator
{
    public int n;
    
    public override TaskStatus Run(UnitController unit, World wm)
    {
        if (status == TaskStatus.None) status = TaskStatus.Running;
        if (Child.Run(unit, wm) == TaskStatus.Failure) status = TaskStatus.Success;

        n += 1;
        return status;
    }
}