using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agent;

public class FoodObject : ResourceObject
{

    public FoodResource food;

    private void Awake()
    {
        Created();
    }

    public override void Created()
    {
        Agent.AgentManager agentManager = FindObjectOfType<Agent.AgentManager>();
        agentManager.NewResource(typeof(FoodResource), transform, food.startAmount);
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
