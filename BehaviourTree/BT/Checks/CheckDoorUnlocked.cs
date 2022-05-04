using UnityEngine;

public class CheckDoorUnlocked : Task
{
    private readonly string doorName;

    public CheckDoorUnlocked(string door)
    {
        doorName = door;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        status = !wm.DoorIsLocked(doorName) ? TaskStatus.Success : TaskStatus.Failure;
        return status;
    }
}