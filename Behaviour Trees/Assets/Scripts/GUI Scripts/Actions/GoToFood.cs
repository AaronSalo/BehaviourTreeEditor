using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToFood : ActionType
{

    public override bool PerformAction(GameObject agent) {
        agent.GetComponent<Behaviour>().debugPanel.transform.GetChild(2).GetComponent<Text>().text = "Going to food";
        Debug.Log("Current Goal: Going to get some food");
        Vector2 foodPos = agent.GetComponent<Hunger>().nearbyFood.transform.position;
        Vector2 agentPos = agent.transform.position;
        Vector2 movement = new Vector2(foodPos.x -agentPos.x, foodPos.y - agentPos.y); // p-p = vec, vector from the agent to the food
        movement *= 20;
        agent.GetComponent<Rigidbody2D>().AddForce(movement);
        //check if we are close enough yet
        if(agent.GetComponent<Behaviour>().nearbyRadius < Vector2.Distance(agentPos, foodPos) ){
            return true; //we're close enough! finished our task
        } else {
            return false; // still too far away from our target
        }
    }
}
