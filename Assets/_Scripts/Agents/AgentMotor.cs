using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    public enum AgentMovingState
    {
        /// <summary>
        /// Agent is moving towards a current target
        /// </summary>
        moving,
        /// <summary>
        /// Agent is doing nothing and is seeking something to do
        /// </summary>
        idle,
        /// <summary>
        /// Agent is currently working on something
        /// </summary>
        busy
    }
    

    public class AgentMotor : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        /// <summary>
        /// The target of the Agent
        /// </summary>
        public Transform Target
        {
            get; set;
        }
        [SerializeField]
        private AgentMovingState agentMovingState;
        public AgentMovingState AgentmovingState { get; set; }
        private void Awake()
        {
            AgentmovingState = AgentMovingState.idle;
        }
        public void Update()
        {
            target = Target;
            agentMovingState = AgentmovingState;
            switch (AgentmovingState)
            {
                case (AgentMovingState.idle):
                    // Try to find a job to do since the Agent Manager has not given the agent a job
                    break;
                case (AgentMovingState.busy):
                    // Keep on working on object
                    break;
                case (AgentMovingState.moving):
                    // Keep moving towards object

                    GetComponent<NavMeshAgent>().destination = Target.position;
                    break;

            }
        }
        



    } 
}
