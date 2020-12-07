using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Behaviour))]
public class BehaviourTree : Editor
{

    public string treePath = "Assets/Resources/nodes.xml";
    BehaviourTreeEditor editor;
    bool loaded = false;
    public void Awake() {

        editor = ScriptableObject.CreateInstance("BehaviourTreeEditor") as BehaviourTreeEditor;

    }


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Behaviour behaviour = (Behaviour) target; 
        
        //open a new editor window
        if( GUILayout.Button("Open Editor") ) {
            BehaviourTreeEditor.ShowWindow();
        }



        //load a tree that we made (or was previously made)
        if( GUILayout.Button("Load Tree") ) {
            //get the tree from a file
            //file browser?
            Load(treePath);
        }
        
        //EditorGUILayout.TextField(treePath);
    }

    private void Load(string path) {
        Debug.Log("Loading nodes...");

        var nodesDeserialized = XMLOp.Deserialize<List<Node>>(path);
        //var connectionsDeserialized = XMLOp.Deserialize<List<Connection>>("Assets/Resources/connections.xml");

        Behaviour behaviour = (Behaviour) target;
        behaviour.nodes = new List<Node>();

        foreach ( var i in nodesDeserialized) {
            behaviour.nodes.Add(i);
        }
    }
}
