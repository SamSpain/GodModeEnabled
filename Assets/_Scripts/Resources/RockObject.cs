using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agent;

public class RockObject : ResourceObject
{
    public RockResource rock;

    private void Awake()
    {
        Created();
    }

    public override void Created()
    {
        Agent.AgentManager agentManager = FindObjectOfType<Agent.AgentManager>();
        agentManager.NewResource(typeof(RockResource), transform, rock.startAmount);
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


