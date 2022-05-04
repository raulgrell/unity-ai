using UnityEngine;
using UnityEngine.AI;

public class AttackTask : Task
{
    private readonly string target;
    
    public AttackTask(string target)
    {
        this.target = target;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        if (status == TaskStatus.None)
        {
            Debug.Log("Begin AttackTask: " + target);
            unit.Attack(target);
            unit.ammo -= 1;
        } 
        
        status = TaskStatus.Success;
        return status;
    }
}
