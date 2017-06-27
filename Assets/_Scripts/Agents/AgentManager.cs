using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Agent
{
    /// <summary>
    /// Controls all agents assigns tasks to the idle
    /// </summary>
    public class AgentManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject siloPrefab;
        [SerializeField]
        private Transform canSiloTransformSpawnPoint;
        [SerializeField]
        private Transform rockSiloTransformSpawnPoint;
        [SerializeField]
        private Transform foodSiloTransformSpawnPoint;

        [SerializeField]
        private GameObject agentPrefab;

        [SerializeField]
        private List<GameObject> houseSpawnPoints;

        [SerializeField]
        private List<GameObject> rockSpawnPoints;

        [SerializeField]
        private List<Building.ResourceSilo> silos = new List<Building.ResourceSilo>();

        [SerializeField]
        private List<AgentController> Agents = new List<AgentController>();

        public List<GameObject> agentSpawns = new List<GameObject>();

        [SerializeField]
        private float
            mediumThreshold = 1000,
            highThreshold = 1000,
            criticalThreshold = 1000;


        public float spawnHumanTimer = 0f;
        public float spawnHumanDelay = 5f;


        public void Start()
        {
            // Make silos
            CreateSilos();
            //CreateFirstAgents();

        }

        private void Update()
        {
            spawnHumanTimer += Time.deltaTime;
            foreach (Building.ResourceSilo silo in silos)
            {
                if(silo.ResourceType == typeof(WoodResource))
                {
                    
                    if(silo.Amount >= 22)
                    {
                        //Debug.Log("Enough wood to build house");
                        foreach(GameObject spawner in houseSpawnPoints)
                        {
                            //Debug.Log(spawner.GetComponent<HouseSpawner>().built);
                            if(!spawner.GetComponent<HouseSpawner>().built)
                            {
                                //Debug.Log("Trying to build house");
                                silo.Withdraw(22);
                                spawner.GetComponent<HouseSpawner>().Build();
                                break;
                            }
                        }
                    }
                }

                if(silo.ResourceType == typeof(RockResource))
                {
                    if(silo.Amount >= 37)
                    {
                        foreach(GameObject spawner in rockSpawnPoints)
                        {
                            if (!spawner.GetComponent<RockSpawner>().built)
                            {
                                silo.Withdraw(37);
                                spawner.GetComponent<RockSpawner>().Build();
                                break;
                            }

                        }
                    }
                }
            }
            if(spawnHumanTimer >= spawnHumanDelay)
            {
                int index = UnityEngine.Random.Range(0, agentSpawns.Count);
                agentSpawns[index].GetComponent<AgentSpawner>().SpawnAgent();
                spawnHumanTimer = 0;
            }
        }

        public void CreateSilos()
        {
            //GameObject canSiloInstance = (GameObject)Instantiate(siloPrefab, canSiloTransformSpawnPoint.position, Quaternion.identity);
            //silos.Add(canSiloInstance.GetComponent<Building.ResourceSilo>());
            //canSiloInstance.GetComponent<Building.ResourceSilo>().Create(typeof(Can), 10);



            GameObject woodSiloInstance = (GameObject)Instantiate(siloPrefab, canSiloTransformSpawnPoint.position, Quaternion.identity);
            silos.Add(woodSiloInstance.GetComponent<Building.ResourceSilo>());
            woodSiloInstance.GetComponent<Building.ResourceSilo>().Create(typeof(WoodResource), 100);


            GameObject rockSiloInstance = (GameObject)Instantiate(siloPrefab, rockSiloTransformSpawnPoint.position, Quaternion.identity);
            silos.Add(rockSiloInstance.GetComponent<Building.ResourceSilo>());
            rockSiloInstance.GetComponent<Building.ResourceSilo>().Create(typeof(RockResource), 100);

            GameObject foodSiloInstance = (GameObject)Instantiate(siloPrefab, foodSiloTransformSpawnPoint.position, Quaternion.identity);
            silos.Add(foodSiloInstance.GetComponent<Building.ResourceSilo>());
            foodSiloInstance.GetComponent<Building.ResourceSilo>().Create(typeof(FoodResource), 100);

        }

        public void CreateFirstAgents()
        {
            GameObject agentInstance = (GameObject)Instantiate(agentPrefab, transform);
            agentInstance.transform.parent = null;
            Agents.Add(agentInstance.GetComponent<AgentController>());
            
        }
        /// <summary>
        /// Agent is seeking new job as it has completed its current job
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public void JobNeeded(AgentController agent)
        {
            try
            {
                //Debug.Log("Job Requested");
                // Go through each silo and find the one with the least percentage, make an importance value from that
                // Go through agent count and make importance on making new agents
                // Compare two and go with highest importance
                // If most importanct one is to collect resources then make list of all objects of that type and find the closest one
                int r = UnityEngine.Random.Range(0, 2);
                if(r == 0)
                {
                    WoodObject[] trees = FindObjectsOfType(typeof(WoodObject)) as WoodObject[];
                    WoodObject bestOne = new WoodObject();
                    foreach (WoodObject tree in trees)
                    {
                        if (bestOne == null || (Vector3.Distance(transform.position, tree.transform.position) < (Vector3.Distance(transform.position, bestOne.transform.position))))
                        {
                            bestOne = tree;
                        }

                    }
                    agent.NewTarget(new AgentTarget(TargetAction.collect, TaskImportance.high, bestOne.transform, typeof(WoodResource)));
                }
                else if(r == 1)
                {
                    RockObject[] rocks = FindObjectsOfType(typeof(RockObject)) as RockObject[];
                    RockObject bestOne = new RockObject();
                    foreach (RockObject rock in rocks)
                    {
                        if (bestOne == null || (Vector3.Distance(transform.position, rock.transform.position) < (Vector3.Distance(transform.position, bestOne.transform.position))))
                        {
                            bestOne = rock;
                        }

                    }
                    agent.NewTarget(new AgentTarget(TargetAction.collect, TaskImportance.high, bestOne.transform, typeof(RockResource)));
                }
                else
                {
                    FoodObject[] foods = FindObjectsOfType(typeof(FoodObject)) as FoodObject[];
                    FoodObject bestOne = new FoodObject();
                    foreach(FoodObject food in foods)
                    {
                        if(bestOne == null || (Vector3.Distance(transform.position, food.transform.position) < (Vector3.Distance(transform.position, bestOne.transform.position))))
                        {
                            bestOne = food;
                        }
                    }
                }

                
            }
            catch
            {
                //Debug.LogWarning("No jobs lol");
            }
            


            
        }

        
        /// <summary>
        /// Agent has requested relevant silo
        /// </summary>
        /// <returns></returns>
        public GameObject SiloNeeded(Type resourceType)
        {
            foreach(Building.ResourceSilo silo in silos)
            {
                if(silo.ResourceType == resourceType)
                {
                    return silo.gameObject;
                }
            }

            //Debug.LogWarning("No suitable silo found.");
            return null;
        }
        /// <summary>
        /// Tells the Agent Manager that there is a new resource available and that it should consider assigning agents.
        /// </summary>
        /// <param name="resourceType">The type of the resource.</param>
        /// <param name="position">Where the resource is in world space.</param>
        /// <param name="amount">How many resouce points does this object have.</param>
        public void NewResource(Type resourceType, Transform target, int amount)
        {
            if(Agents.Count == 0)
            {
                return;
            }
            //Debug.Log("New resource detected");
            TaskImportance taskImportance = TaskImportance.low;
            foreach(Building.ResourceSilo silo in silos)
            {
                if(silo.ResourceType == resourceType)
                {
                    
                    int siloPercentage = (silo.Amount / silo.MaxAmount) * 100;

                    if (siloPercentage < 10)
                    {
                        //Debug.Log("Set task to critical.");
                        taskImportance = TaskImportance.critical;
                    }
                    else if(siloPercentage > 90)
                    {
                        //Debug.Log("Set task to low.");
                        taskImportance = TaskImportance.low;
                    }
                    else
                    {
                        //Debug.Log("Set task to medium.");
                        taskImportance = TaskImportance.medium;
                    }
                }
                
            }

            if(taskImportance == TaskImportance.low || taskImportance == TaskImportance.filler)
            {
                //Debug.Log("New resource is low importance.");
                return;
            }


            // Calculate if any agents are anywhere near the new resource
            foreach(AgentController agent in Agents)
            {
                //Debug.Log("Looking at " + agent.name);
                float distance = Vector3.Distance(agent.transform.position, target.position);

                // If the task importance does not exceed the distance threshold
                switch(taskImportance)
                {
                    case (TaskImportance.critical):
                        if(distance > criticalThreshold)
                        {
                            //Debug.Log("Too far away.");
                            return;
                        }
                        break;
                    case (TaskImportance.high):
                        if(distance > highThreshold)
                        {
                            //Debug.Log("Too far away.");
                            return;
                        }
                        break;
                    case (TaskImportance.medium):
                        if(distance > mediumThreshold)
                        {
                            //Debug.Log("Too far away.");
                            return;
                        }
                        break;
                }
                // Remember, enums are numbers and the higher the number it is the more important
                if(agent.CurrentTarget == null || agent.AgentState == AgentState.idling || taskImportance > agent.CurrentTarget.TaskImportance)
                {
                    //Debug.Log("New job for agent");
                    agent.NewTarget(new AgentTarget(TargetAction.collect, taskImportance, target, resourceType));
                    return;
                }
                else
                {
                    //Debug.Log("Only current agents have important job.");
                }

                
                
            }
            //Debug.Log("No suitable agent found");
            
        }
        /// <summary>
        /// Add new Agent to available list of agents in the pool
        /// </summary>
        public void RegisterAgent(AgentController agent)
        {
            if(Agents.Count != 0)
            {
                foreach (AgentController existingAgent in Agents)
                {
                    if (agent.transform == existingAgent.transform)
                    {
                        Debug.LogWarning("Existing agent attempting to register again to list of agents in " + this + ".");
                        return;
                    }
                }
            }
            
            Agents.Add(agent);
        }
        /// <summary>
        /// Remove agent from list of agents in the pool
        /// </summary>
        /// <param name="agent"></param>
        private void RemoveAgent(AgentController agent)
        {
            foreach(AgentController existingAgent in Agents)
            {
                if(agent.transform == existingAgent.transform)
                {
                    Agents.Remove(existingAgent);
                }
            }
            //Debug.LogWarning("Agent that does not exist in list of agents in " + this + " tried to remove itself.");
        }
        
    }
}
