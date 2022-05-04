using UnityEngine;
using UnityEngine.AI;

public class OpenDoorTask : Task
{
    private readonly string doorName;
    
    public OpenDoorTask(string door)
    {
        doorName = door;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        Debug.Log("Begin OpenDoorTask");
        if (!wm.DoorIsOpen(doorName)) wm.OpenDoor(doorName);
        
        status = TaskStatus.Success;
        return status;
    }
}