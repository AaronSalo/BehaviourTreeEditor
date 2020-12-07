using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{   
    //how many hunger points to be given when eaten
    public float hungerPoints = 70;

    public float Eat() {
        return hungerPoints;
    }
}
