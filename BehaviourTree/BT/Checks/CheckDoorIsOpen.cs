using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoorIsOpen : Task
{
    private readonly string doorName;

    public CheckDoorIsOpen(string door)
    {
        doorName = door;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        status = wm.DoorIsOpen(doorName) ? TaskStatus.Success : TaskStatus.Failure;
        Debug.Log("DoorOpenCondition: " + status);
        return status;
    }
}