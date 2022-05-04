using UnityEngine;
using UnityEngine.AI;

public class BargeDoorTask : Task
{
    private readonly string doorName;
    
    public BargeDoorTask(string door)
    {
        doorName = door;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        Debug.Log("Begin BargeDoorTask: " + doorName);
        if (!wm.DoorIsOpen(doorName))
        {
            wm.BargeDoor(doorName);
        }
        
        status = TaskStatus.Success;
        return status;
    }
}