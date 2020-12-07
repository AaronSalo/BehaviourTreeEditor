using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Xml.Serialization;

public class ActionNode : Node
{

    [XmlIgnore] public MonoScript actionField = null;
    public String actionName;
    //public ActionType action;
    public delegate void ActionEventHandler(object source);

    public event ActionEventHandler ActionHappened;


    //for saving
    public ActionNode() {}

    //calls the parent constructor... We dont care how the node is drawn or anyhitng
    public ActionNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, 
    GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, 
    Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) 
    : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode) {}




    public override StateType Tick(GameObject agent)
    {
        System.Type monoType = Type.GetType(actionName); //get the type
        agent.AddComponent(monoType); //add the component to the agent
        ActionType action = agent.GetComponent(monoType) as ActionType;

        //perform the action
        bool state = action.PerformAction(agent);
        
        if(state)
            return StateType.SUCCESS;
        else
            return StateType.FAILURE;
    }

    public void OnAction() {
        if(ActionHappened != null)
            ActionHappened(this);
    }


    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
        GUI.Label(new Rect(rect.x + rect.width/6, rect.y -rect.width/8, rect.width, rect.height) , "Action");

        Rect objRect = new Rect(rect.x + 20, rect.y+ rect.width/5 , 100, 20);
        actionField = EditorGUI.ObjectField(objRect, actionField, typeof(MonoScript) , true) as MonoScript;
        if(actionField != null)
            actionName = actionField.GetClass().ToString();
        //Debug.Log(actionName);
       // Debug.Log(actionField.GetClass());
        // if(action != null)
        //     action = Activator.CreateInstance(actionField.GetClass()) as ActionType;
    }


    protected override void ProcessContextMenu(){
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

}
