using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public Vector3 MinRange = new Vector3(4, 0.2f, -3);
    public Vector3 MaxRange = new Vector3(-4, 0.2f, 4);

    Task Root;

	// Use this for initialization
	void Start () {
        
        //Setup the behaviour tree
        var root = new Sequencer();
        root.Children = new Task[2];
        root.Children[0] = new Roam(MinRange, MaxRange);
        root.Children[1] = new Idle();

        Root = root;

    }
	
	// Update is called once per frame
	void Update () {
        Root.Execute();
	}
}
