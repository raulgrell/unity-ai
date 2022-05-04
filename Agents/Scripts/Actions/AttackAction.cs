using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Attack", fileName = "AttackAction")]
public class AttackAction : StateAction
{
    public GameObject prefab;
    public float timeInterval;
    public float energyCost;
    private float speed = 10f;

    private float timer;

    public override void Act(FiniteStateMachine fsm)
    {
        if (timer > timeInterval)
        {
            timer = 0;
            var agent = fsm.transform;
            var position = agent.position;
            var target = fsm.Agent.Target;
            var projectile = Instantiate(prefab, position + agent.forward, Quaternion.identity);
            var body = projectile.GetComponent<Rigidbody>();
            body.velocity = (target.position - position).normalized * speed;

            fsm.Agent.Energy -= energyCost * Time.deltaTime;
        }
        
        timer += Time.deltaTime;
    }
}