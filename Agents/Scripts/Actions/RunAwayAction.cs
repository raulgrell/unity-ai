using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 [CreateAssetMenu(menuName = "FSM/Actions/Run Away", fileName = "RunAwayAction")]
 public class RunAwayAction : StateAction
 {
     public float energyCost;
     
     public override void Act(FiniteStateMachine fsm)
     {
         fsm.Agent.GoToRecovery();
         fsm.Agent.Energy -= energyCost * Time.deltaTime;
     }
 }