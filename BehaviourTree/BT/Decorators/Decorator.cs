using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : Task
{
    protected Task Child => children.Count > 0 ? children[0] : null; 
    
    public override void AddChild(Task task)
    {
        if (children.Count > 0)
            children[0] = task;
        else
            children.Add(task);
    }
}