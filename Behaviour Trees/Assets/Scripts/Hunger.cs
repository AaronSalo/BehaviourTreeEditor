using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{

    public float hunger = 100.0f; //full
    public float hungerDecreaseRate = 10;

    public GameObject nearbyFood;

    //set in start from behaviour script
    private float nearbyRadius;
    private GameObject debugPanel;
    // Start is called before the first frame update
    void Start()
    {
        
        nearbyRadius = gameObject.GetComponent<Behaviour>().nearbyRadius;
        debugPanel = gameObject.GetComponent<Behaviour>().debugPanel;
        InvokeRepeating("DecreaseHunger", 1, 1); //decrease hunger every second
    }


public bool EatFood() {
        if(nearbyFood != null && Vector2.Distance(transform.position, nearbyFood.transform.position) < nearbyRadius) {
            hunger += nearbyFood.GetComponent<Food>().Eat();
            Destroy(nearbyFood);
            debugPanel.transform.GetChild(2).GetComponent<Text>().text = "Current Goal: Eat food";
            Debug.Log("Agent ate some food");
            return true;
        } else {
            return false; //failure. could not successfully eat food
        }
    }

    //when hunger is less than 40, we are hungry
    public bool IsHungry() {
        return hunger <= 40;
    }


    public void DecreaseHunger() {
        if(hunger > 0)
            hunger -= hungerDecreaseRate;
        debugPanel.transform.GetChild(0).GetComponent<Text>().text = "Hunger: " + hunger;
        debugPanel.transform.GetChild(1).GetComponent<Text>().text = "Hungry? " + IsHungry();
    }

}