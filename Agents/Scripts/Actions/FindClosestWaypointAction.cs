using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 [CreateAssetMenu(menuName = "FSM/Actions/FindClosestWaypoint", fileName = "FindClosestWaypoint")]
 public class FindClosestWaypointAction : StateAction
 {
     public override void Act(FiniteStateMachine fsm)
     {
         fsm.Agent.GoToClosestWaypoint(fsm.Agent.transform.position);
     }
 }