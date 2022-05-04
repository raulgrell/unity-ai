using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace Planning
{
    [Serializable]
    public struct Waypoint
    {
        public string name;
        public Transform waypoint;
    }

    public abstract class ActionPlanner : MonoBehaviour
    {
        public string problemName;
        public List<PlanAction> plan = new List<PlanAction>();
        public List<Waypoint> waypoints = new List<Waypoint>();
        
        protected NavMeshAgent agent;

        private int actionIndex;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            actionIndex = 0;

            if (string.IsNullOrEmpty(problemName))
            {
                return;
            }

            try
            {
                var assetPath = Application.dataPath;
                var exePath = $"{assetPath}/Planning/planner/hsp2.exe";
                var problemPath = $"\"{assetPath}/Planning/{problemName}/problem.pddl\"";
                var domainPath = $"\"{assetPath}/Planning/{problemName}/domain.pddl\"";

                Process plannerProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = exePath,
                        Arguments = $"{problemPath} {domainPath}",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    }
                };

                plannerProcess.Start();
                plannerProcess.WaitForExit();

                while (!plannerProcess.StandardOutput.EndOfStream)
                {
                    plan.Add(new PlanAction(plannerProcess.StandardOutput.ReadLine()));
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }

            foreach (PlanAction planAction in plan)
            {
                string parameters = "";
                foreach (string param in planAction.parameters)
                {
                    parameters += " " + param;
                }
                Debug.Log($"({planAction.name}{parameters})");
            }
        }

        private void Update()
        {
            if (actionIndex >= plan.Count)
                return;

            var action = plan[actionIndex];

            switch (action.status)
            {
                case Status.Ready:
                    DoAction(action);
                    break;
                case Status.Running:
                    CheckAction(action);
                    break;
                case Status.Complete:
                    actionIndex += 1;
                    break;
            }
        }

        public abstract void DoAction(PlanAction action);


        public abstract void CheckAction(PlanAction action);

        public Vector3 GetWaypoint(string name)
        {
            foreach (Waypoint waypoint in waypoints)
            {
                if (waypoint.name.Equals(name))
                    return waypoint.waypoint.position;
            }

            return transform.position;
        }

        public bool isAtDestination()
        {
            return !agent.pathPending
                   && agent.remainingDistance <= agent.stoppingDistance
                   && (!agent.hasPath || agent.velocity.sqrMagnitude == 0);
        }
    }
}