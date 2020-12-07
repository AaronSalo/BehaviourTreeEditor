using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ACTION
// checks if food is close enough that we can eat it
public class FoodInRange : ActionType
{

    public override bool PerformAction(GameObject agent) {

        bool result = Vector2.Distance(agent.transform.position, agent.GetComponent<Hunger>().nearbyFood.transform.position)
            < agent.GetComponent<Behaviour>().nearbyRadius;

        Debug.Log("Is the food close enough for the agent to eat?:" + result);
        return result;
    }
}
