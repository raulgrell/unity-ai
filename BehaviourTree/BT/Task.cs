using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus
{
    None,
    Success,
    Failure,
    Running
};

public abstract class Task
{
    protected readonly List<Task> children = new List<Task>();
    public TaskStatus status = TaskStatus.None;

    public List<Task> Children => children;

    public abstract TaskStatus Run(UnitController unit, World wm);

    public virtual void AddChild(Task task)
    {
        children.Add(task);
    }

    public void SetParent(Task task)
    {
        task.AddChild(this);
    }
}