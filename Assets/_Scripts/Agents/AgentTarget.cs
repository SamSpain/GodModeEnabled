using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Agent
{
    
    public enum TargetAction
    {
        collect,
        move,
        use,
        deposit,
        mate,
        waiting
    }
    public enum TaskImportance
    {
        waiting,
        /// <summary>
        /// Do these tasks to make the AI seem alive and doing things
        /// </summary>
        filler,
        low,
        medium,
        high,
        /// <summary>
        /// Do these tasks above all else to keep everyone alive
        /// </summary>
        critical
    }

    [System.Serializable]
    /// <summary>
    /// Responsible for the targets the agents need to interact with.
    /// Gives required actions and results of actions
    /// </summary>
    public class AgentTarget
    {
        public static AgentTarget Default
        {
            get
            {
                return new AgentTarget(TargetAction.waiting, TaskImportance.waiting, null, null);
            }
        }
        public Transform Target { get; set; }
        public TargetAction TargetAction { get; private set; }
        public TaskImportance TaskImportance;
        public Type TaskResource { get; private set; }
        public AgentTarget(TargetAction targetAction, TaskImportance taskImportance, Transform target, Type taskResource)
        {
            TargetAction = targetAction;
            TaskImportance = taskImportance;
            Target = target;
            TaskResource = taskResource;
        }

    }
}
