using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ACTION
// sees if the agent knows where some food is
public class FoodNearby : ActionType
{

    public override bool PerformAction(GameObject agent) {
        bool result =  agent.GetComponent<Hunger>().nearbyFood != null;
        Debug.Log("Does the agent know where some food is?: " + result);
        return result;
    }
}
