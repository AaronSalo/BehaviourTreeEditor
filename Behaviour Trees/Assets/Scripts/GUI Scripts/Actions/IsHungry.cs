using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ACTION
// checks if the agent is hungry by asking its hunger component
public class IsHungry : ActionType
{

    public override bool PerformAction(GameObject agent)
    {
        bool result = agent.GetComponent<Hunger>().IsHungry();
        Debug.Log("Agent is hungry? " + result);
        return result;
    }
}
