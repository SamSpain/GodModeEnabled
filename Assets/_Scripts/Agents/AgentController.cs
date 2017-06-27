using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    public enum AgentState
    {
        working,
        moving,
        idling
    }
    /// <summary>
    /// Works as Agent AI behaviour to give the Agent a target
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AgentMotor))]
    public class AgentController : MonoBehaviour
    {
        public GameObject myTarget;
        private AgentManager Manager { get; set; }
        public AgentTarget CurrentTarget;
        public AgentState AgentState { get; set; }
        public float Food { get; set; }
        public float Happiness { get; set; }
        public float Wear { get; set; }
        public int StorageLimit { get; set; }
        public int StorageAmount { get; set; }
        public Animator animator;
        
        public float CollectCounter { get; set; }
        public float DepositCounter { get; set; }
        /// <summary>
        /// How long it takes for the agent to collect another resource point from a resource object
        /// </summary>
        private float collectTimer = 1f;
        private float depositTimer = 1f;

        

        private void Start()
        {
            Manager = FindObjectOfType<AgentManager>();
            CollectCounter = 0;
            StorageLimit = 5;
            Manager.RegisterAgent(this);
            CurrentTarget = AgentTarget.Default;
            Manager.JobNeeded(this);
            GetComponent<AgentMotor>().AgentmovingState = AgentMovingState.moving;
            animator.SetTrigger("Walk");
            AgentState = AgentState.moving;

        }
        /// <summary>
        /// Tells Agent to interact with new target.
        /// </summary>
        /// <param name="target"></param>
        public void NewTarget(AgentTarget target)
        {
            CurrentTarget = target;
            GetComponent<AgentMotor>().Target = target.Target;
            GetComponent<AgentMotor>().AgentmovingState = AgentMovingState.moving;
            animator.SetTrigger("Walk");
            AgentState = AgentState.moving;

        }

        /// <summary>
        /// Tells Agent to find new suitable target.
        /// This is more of a polish thing when we have filler tasks.
        /// </summary>
        public void NewTarget()
        {
            
        }

        public void Update()
        {
            
            switch(AgentState)
            {
                case (AgentState.working):
                    Work();
                    break;
                case (AgentState.moving):
                    if(ValidateTarget())
                    {
                        if(GetComponent<AgentMotor>().Target != CurrentTarget.Target)
                        {
                            GetComponent<AgentMotor>().Target = CurrentTarget.Target;
                        }

                        if(Vector3.Distance(transform.position, CurrentTarget.Target.position) < 1)
                        {
                            StartWorking();
                        }
                    }

                    break;
                case (AgentState.idling):
                    
                    break;
            }
        }
        public void StartWorking()
        {
            GetComponent<AgentMotor>().AgentmovingState = AgentMovingState.busy;
            animator.SetTrigger("Stand");
            AgentState = AgentState.working;
        }
        public void Work()
        {
            switch(CurrentTarget.TargetAction)
            {
                case (TargetAction.collect):
                    CollectCounter += Time.deltaTime;
                    if(CollectCounter >= collectTimer)
                    {
                        Collect();
                        CollectCounter = 0;
                    }
                    
                    break;
                case (TargetAction.deposit):
                    DepositCounter += Time.deltaTime;
                    if(DepositCounter >= depositTimer)
                    {
                        Deposit();
                        DepositCounter = 0;
                        
                    }
                    
                    if(StorageAmount <= 0)
                    {
                        //Debug.Log("Jobs done.");
                        CurrentTarget = AgentTarget.Default;
                        GetComponent<AgentMotor>().AgentmovingState = AgentMovingState.idle;
                        animator.SetTrigger("Stand");
                        Manager.JobNeeded(this);

                    }
                    
                    break;
                case (TargetAction.mate):
                    // Interact with target to mate
                   
                    break;
            }
        }

        /// <summary>
        /// Store resource on Agent after it has called Collect on resource object
        /// </summary>
        public void StoreOnAgent(int amount)
        {
            StorageAmount += amount;
            if(StorageAmount >= StorageLimit)
            {
                //Debug.Log("Player ran out of storage. Looking for silo");
                // Find a silo to move to and deposit resources
                AgentTarget newTarget = new AgentTarget
                    (
                    TargetAction.deposit,
                    TaskImportance.high,
                    Manager.SiloNeeded(CurrentTarget.TaskResource).transform,
                    CurrentTarget.TaskResource
                    );
                NewTarget(newTarget);
                
                
            }
        }

        public void Collect()
        {
            //Debug.Log("Collect");
            CurrentTarget.Target.GetComponent<ResourceObject>().Withdraw(this, 1);
        }
        public void Deposit()
        {
            //Debug.Log("Deposit");
            CurrentTarget.Target.GetComponent<Building.ResourceSilo>().Deposit(1);
            StorageAmount--;
        }
        public void DepletedTarget()
        {
            //Debug.Log("Target was depleted, looking for new target.");
            AgentTarget newTarget = new AgentTarget
                    (
                    TargetAction.deposit,
                    CurrentTarget.TaskImportance,
                    Manager.SiloNeeded(CurrentTarget.TaskResource).transform,
                    CurrentTarget.TaskResource
                    );
            NewTarget(newTarget);
        }
        /// <summary>
        /// Check if the target is acceptable to walk towards and use in any way
        /// </summary>
        public bool ValidateTarget()
        {
            return true;
        }
        
    } 
}
