using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Sequence : Node
{


    //for saving
    public Sequence() {}

    //calls the parent constructor... We dont care how the node is drawn or anyhitng
    public Sequence(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, 
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
        GUI.Label(new Rect(rect.x + rect.width/6, rect.y , rect.width, rect.height) , "->");
    }

    public override string ToString() {
        return "this is a SEQUENCE NODE";
    }

    //tick children in sequence
    // agent: the agent performing the action
    public override StateType Tick(GameObject agent) {
        StateType childStatus = StateType.SUCCESS;

        //Debug.Log("In sequence");

        foreach(Node i in children) {
            
            //tick the children
            childStatus = i.Tick(agent);

            if(childStatus == StateType.RUNNING) {
                return StateType.RUNNING;
            } else if (childStatus == StateType.FAILURE) {
                return StateType.FAILURE;
            }
        } //foreach

        Debug.Log("All Children succeeded in sequence");
        nodeState = StateType.SUCCESS;
        return childStatus; 
    }

    protected override void ProcessContextMenu(){
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }
}
