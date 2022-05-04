using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 [CreateAssetMenu(menuName = "FSM/Actions/Recover", fileName = "RecoverAction")]
 public class RecoverAction : StateAction
 {
     public float energyCost;
     
     public override void Act(FiniteStateMachine fsm)
     {
         if (fsm.Agent.isAtDestination())
            fsm.Agent.Energy -= energyCost * Time.deltaTime;
     }
 }