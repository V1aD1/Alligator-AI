using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float MaxMovementSpeed = 1f;
    public float MinDistanceUntilDestinationReached = 0.1f;
    public Vector2 MinRange = new Vector2(4, -3);
    public Vector2 MaxRange = new Vector2(-4, 4);
    public GameObject ActorToMove;

    private Vector3 destination = Vector3.zero;
    private Task Root;

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
            Debug.Assert(value.y == 0f, "Destination must always have a y value of 0!!");
            if (value.y == 0f && 
                MathHelper.IsVectorWithinRange(new Vector3(MinRange.x, 0f, MinRange.y), new Vector3(MaxRange.x, 0f, MaxRange.y), value))
            {
                destination = new Vector3(value.x, 0f, value.z);
            }
        }
    }

    public Animator ActorAnimator { get; private set; }

    void Awake()
    {
        ActorAnimator = ActorToMove.GetComponentInChildren<Animator>();    
    }

    // Use this for initialization
    void Start () {
        float actorForwardAxisLength = ActorToMove.GetComponent<BoxCollider>().bounds.size.z;

        //the 4* multiplier is abit arbitrary
        float recommendedAreaDiagonalLength = 4 * actorForwardAxisLength;

        Debug.Assert(ActorToMove != null, "ActorToMove not set!");
        Debug.Assert(ActorAnimator != null, "ActorAnimator not set!");
        Debug.Assert((MinRange - MaxRange).magnitude > recommendedAreaDiagonalLength, 
                     "It is recommended that the distance between the corners of the walkable area is at least: "
                     +recommendedAreaDiagonalLength + "so as to avoid strange behaviour.");

        //Setup the behaviour tree
        var root = new Sequencer();
        root.Children = new Task[2];
        root.Children[0] = new Roam(MinRange, MaxRange, ActorToMove.GetComponent<BoxCollider>().bounds.size.z);
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
