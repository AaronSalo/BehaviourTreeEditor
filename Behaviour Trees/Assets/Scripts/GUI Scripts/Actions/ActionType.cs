using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionType : MonoBehaviour
{

    public virtual bool PerformAction(GameObject agent) {
        Debug.LogError("Default action not overridden");
        return true;
    }
}