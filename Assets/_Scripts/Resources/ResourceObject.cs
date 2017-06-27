using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script attached to the object which tells the Agent Manager it has been created
/// </summary>
public abstract class ResourceObject : MonoBehaviour
{
    public int ResourceAmount;
    public abstract void Created();

    public virtual void Withdraw(Agent.AgentController agent, int withdrawAmount)
    {
        //Debug.Log("Withdraw");
        if(withdrawAmount <= ResourceAmount)
        {
            ResourceAmount -= withdrawAmount;
            agent.StoreOnAgent(withdrawAmount);
        }
        else
        {
            agent.DepletedTarget();
            
        }
    }

    
}
