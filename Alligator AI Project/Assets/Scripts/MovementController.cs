using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public readonly float MaxSpeed = 5f;
    public readonly Vector3 MinRange = new Vector3(4, 0.2f, -3);
    public readonly Vector3 MaxRange = new Vector3(-4, 0.2f, 4);
    public GameObject ActorToMove;

    Vector3 destination = Vector3.zero;
    Task Root;

    /// <summary>
    /// destination will only be set if within range specified by MinRange and MaxRange
    /// </summary>
    public Vector3 Destination
    {
        get
        {
            return destination;
        }

        set
        {
            if (MathHelper.IsVectorWithinRange(MinRange, MaxRange, value))
            {
                destination = value;
            }
        }
    }

	// Use this for initialization
	void Start () {

        Debug.Assert(ActorToMove != null, "ActorToMove not set!");

        //TODO add assert for animation controller


        //Setup the behaviour tree
        var root = new Sequencer();
        root.Children = new Task[2];
        root.Children[0] = new Roam(MinRange, MaxRange);
        root.Children[1] = new Idle();

        Root = root;

    }
	
	// Update is called once per frame
	void Update () {
        Root.Execute(ActorToMove, this);
        Debug.DrawLine(ActorToMove.transform.position, destination, Color.red);
        Debug.Log("Current destination: " + Destination);
	}
}
