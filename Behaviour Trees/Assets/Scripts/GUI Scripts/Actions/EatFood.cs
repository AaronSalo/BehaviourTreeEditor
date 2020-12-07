using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ACTION
// tries to eat food
public class EatFood : ActionType
{
    public override bool PerformAction(GameObject agent) {
        bool result = agent.GetComponent<Hunger>().EatFood();
        Debug.Log("Agent ate some food? " + result);
        return result;
    }
}
