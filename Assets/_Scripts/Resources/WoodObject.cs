using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agent;

public class WoodObject : ResourceObject
{

    /// <summary>
    /// But can the can can can?
    /// </summary>
    public WoodResource wood;


    public void Awake()
    {
        Created();
    }
    public override void Created()
    {
        Agent.AgentManager agentManager = FindObjectOfType<Agent.AgentManager>();
        agentManager.NewResource(typeof(WoodResource), transform, wood.startAmount);
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
