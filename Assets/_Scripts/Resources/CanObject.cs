using System;
using System.Collections;
using System.Collections.Generic;
using Agent;
using UnityEngine;

public class CanObject : ResourceObject
{
    /// <summary>
    /// But can the can can can?
    /// </summary>
    public Can can;


    public void Awake()
    {
        Created();
    }
    public override void Created()
    {
        Agent.AgentManager agentManager = FindObjectOfType<Agent.AgentManager>();
        agentManager.NewResource(typeof(Can), transform, can.startAmount);
    }
    public override void Withdraw(AgentController agent, int withdrawAmount)
    {
        base.Withdraw(agent, withdrawAmount);
        if (ResourceAmount <= 0)
        {
            gameObject.SetActive(false);
        }

    }


}
