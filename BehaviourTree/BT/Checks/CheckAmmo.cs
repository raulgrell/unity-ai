using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAmmo : Task
{
    private readonly int limit;

    public CheckAmmo(int limit)
    {
        this.limit = limit;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        status = unit.ammo < limit ? TaskStatus.Success : TaskStatus.Failure;
        return status;
    }
}