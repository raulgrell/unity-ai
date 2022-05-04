using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BehaviourTree<T> : MonoBehaviour where T : UnitController
{
    [SerializeField] private World world;
    [SerializeField] private Task root;

    private TaskStatus status = TaskStatus.None;
    private Task current;

    private T unit;

    protected void Start()
    {
        unit = GetComponent<T>();
        current = root;
    }

    void Update()
    {
        if (status == TaskStatus.None || status == TaskStatus.Running)
            status = current.Run(unit, world);
    }    
}
