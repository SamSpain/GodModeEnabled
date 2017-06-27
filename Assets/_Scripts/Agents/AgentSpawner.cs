using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public GameObject ManPrefab;
    public GameObject WomenPrefab;


    public GameObject SpawnAgent()
    {
        GameObject agentInstance;

        int gender = Random.Range(0, 2);
        if(gender == 0)
        {
            // Spawn men
            agentInstance = (GameObject)Instantiate(ManPrefab, transform.position, Quaternion.identity);

            
        }
        else
        {
            // Spawn women
            agentInstance = (GameObject)Instantiate(WomenPrefab, transform.position, Quaternion.identity);
        }

        return agentInstance;
    }
	
}
