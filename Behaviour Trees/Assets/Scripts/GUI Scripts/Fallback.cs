using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Fallback : Node
{

    public Fallback() {}

    public Fallback(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, 
    GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, 
    Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) 
    : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, 
    OnClickOutPoint, OnClickRemoveNode) 
    {}
    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
        GUI.Label(new Rect(rect.x + rect.width/6, rect.y , rect.width, rect.height) , "?");
    }

    //tick children till we get a success
    // agent: the agent performing the action
    public override StateType Tick(GameObject agent) {
        StateType childStatus = StateType.SUCCESS;

        //Debug.Log("In fallback");

        foreach(Node i in children) {
            
            //tick the children
            childStatus = i.Tick(agent);

            if(childStatus == StateType.RUNNING) {
                return StateType.RUNNING;
            } else if (childStatus == StateType.SUCCESS) {
                return StateType.SUCCESS;
            }
        } //foreach

        Debug.Log("All Children failed in fallback");
        nodeState = StateType.FAILURE;
        return childStatus; 
    }

    protected override void ProcessContextMenu(){
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }
}
