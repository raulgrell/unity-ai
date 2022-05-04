using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : Task
{
    public override TaskStatus Run(UnitController unit, World wm)
    {
        return TaskStatus.Success;
    }
}