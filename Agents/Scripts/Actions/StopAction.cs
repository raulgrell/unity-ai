using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 [CreateAssetMenu(menuName = "FSM/Actions/Stop", fileName = "StopAction")]
 public class StopAction : StateAction
 {
     public override void Act(FiniteStateMachine fsm)
     {
         fsm.Agent.Stop();
     }
 }