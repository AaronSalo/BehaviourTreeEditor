using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ACTION
// find some food within the lookradius
public class FindFood : ActionType
{
    public float lookRadius = 20.0f;
    public GameObject[] foodNearby;

    public override bool PerformAction(GameObject agent) {
        
        LayerMask mask = LayerMask.GetMask("Food");
        Vector2 agentPos = agent.transform.position;
        Collider2D[] food = Physics2D.OverlapCircleAll(agentPos, lookRadius, mask);

        if(food.Length > 0) {

            GameObject closestFood = food[0].gameObject;
            float closestDist = Vector2.Distance(agentPos, closestFood.transform.position);
            
            //find the closest food
            for(int i = 1; i < food.Length; i++) {
                float checkDist = Vector2.Distance(agentPos, food[i].transform.position);
                if(checkDist < closestDist) {
                    closestFood = food[i].gameObject;
                    closestDist = checkDist;
                }
            }
            
            agent.GetComponent<Hunger>().nearbyFood = closestFood;
            Debug.Log("Agent found some food!");
            return true; // we found food! success!!!
        } else {
            Debug.Log("Agent could not find food.");
            return false; // no food nearby
        }
    }

}
