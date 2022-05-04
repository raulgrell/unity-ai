using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failure : Task
{
    public override TaskStatus Run(UnitController unit, World wm)
    {
        return TaskStatus.Failure;
    }
}