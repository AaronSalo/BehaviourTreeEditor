using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{

    public float nearbyRadius = 3;
    public GameObject debugPanel;

    public List<Node> nodes;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateTick", 1, 2f); // call Generate tick every second

    }


    public void SetNodes(List<Node> nodes) {
        this.nodes = nodes;
    }


    public void GenerateTick() {
        if(nodes == null) {
            Debug.Log("Tree is empty; Cannot Generate" );
        } else {
            nodes[0].GenerateTick(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, nearbyRadius);
    }
}
