using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Xml.Serialization;

[XmlInclude(typeof(Sequence))]
[XmlInclude(typeof(ActionNode))]
[XmlInclude(typeof(Fallback))]
public class Node
{
    //TICK VARIABLES
    //the possible return states from a tick
    public enum StateType {FAILURE, RUNNING, SUCCESS}

    [XmlIgnore] public StateType nodeState;

    public List<Node> children;
    public List<StateType> childStates;

    //DRAWING VARIABLES
    public Rect rect;
    [XmlIgnore] public string title;
    [XmlIgnore] public bool isDragged;
    [XmlIgnore] public bool isSelected;

    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;

    [XmlIgnore] public GUIStyle style;
    [XmlIgnore] public GUIStyle defaultNodeStyle;
    [XmlIgnore] public GUIStyle selectedNodeStyle;

    [XmlIgnore] public Action<Node> OnRemoveNode;

    //public Texture2D image;


    public Node() 
    {
    }


    //for loading
    public Node(List<Node> children) {
        this.children = children;
    }

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, 
        GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, 
        Action<Node> OnClickRemoveNode) 
    {
        rect = new Rect(position.x, position.y, width, height);
        inPoint = new ConnectionPoint(this, ConnectionPointType.IN, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.OUT, outPointStyle, OnClickOutPoint);

        style = nodeStyle;
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;

        children = new List<Node>();
        //image = Resources.Load("Assets//Sprites/null.png", typeof(Image)) as Texture2D;
    }


    //load constructor
	public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, 
        GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint,
		Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, string inPointID, string outPointID)
	{
		rect = new Rect(position.x, position.y, width, height);
		style = nodeStyle;
		inPoint = new ConnectionPoint(this, ConnectionPointType.IN, inPointStyle, OnClickInPoint, inPointID);
		outPoint = new ConnectionPoint(this, ConnectionPointType.OUT, outPointStyle, OnClickOutPoint, outPointID);
		defaultNodeStyle = nodeStyle;
		selectedNodeStyle = selectedStyle;
		OnRemoveNode = OnClickRemoveNode;
	}

    //Generate a tick
    // agent: the agent performing the action
    public void GenerateTick(GameObject agent) {
        Tick(agent);
    }    
    //the purpose of the root is to simply generate ticks. Should only have one child
    // agent: the agent performing the action
    public virtual StateType Tick(GameObject agent) {
        nodeState = StateType.RUNNING;
        Debug.Log("Generated Tick from ROOT");

        //SHOULD just be one... but you never know
        //this will behave similar to a seqence node, without caring for status
        foreach (Node i in children){  
            i.Tick(agent);
            nodeState = i.nodeState;
        }
        return nodeState;
    }

    //enables dragging of nodes
    public void Drag(Vector2 delta) {
        rect.position += delta; //add the change in position
    }

    public virtual void Draw() {
        //inPoint.Draw(); //the root node has no inpoint
        outPoint.Draw();
        GUI.Box(rect, title, style);
        GUI.Label(new Rect(rect.x + rect.width/6, rect.y , rect.width, rect.height) , "ROOT");
    }


    public bool ProcessEvents(Event e) {
        switch(e.type)
        {
            
            case EventType.MouseDown: 
                if(e.button == 0) {
                    if(rect.Contains(e.mousePosition)) {
                        isDragged = true;
                        GUI.changed = true; //set a flag to update the GUI
                        isSelected = true;
                        style = selectedNodeStyle;
                    } else {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if(e.button == 1 && isSelected) {
                    ProcessContextMenu();
                    e.Use();
                }
                break;
            
            //let go of the node
            case EventType.MouseUp:
                isDragged = false;
                break;

            //Currently dragging the node
            case EventType.MouseDrag:
                if(e.button == 0 && isDragged)
                {
                    Drag(e.delta); //change in mouse position
                    e.Use();
                    return true;
                }
                break;
        }
        return false;
    }

    protected virtual void ProcessContextMenu(){
        GenericMenu genericMenu = new GenericMenu();
        int ret;
        //genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        //genericMenu.AddItem(new GUIContent("Generate Tick"), false, GenerateTick);
        genericMenu.ShowAsContext();
    }

    protected void OnClickRemoveNode() {
        if(OnRemoveNode != null)
            OnRemoveNode(this);
    }


}//Node
