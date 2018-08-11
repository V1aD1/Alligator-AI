using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float MaxSpeed = 1f;
    public float MinDistanceUntilDestinationReached = 0.1f;
    public Vector2 MinRange = new Vector2(4, -3);
    public Vector2 MaxRange = new Vector2(-4, 4);
    public GameObject ActorToMove;

    private Vector3 destination = Vector3.zero;
    private Task Root;
    private Animator actorAnimator;

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

    public Animator ActorAnimator
    {
        get
        {
            return actorAnimator;
        }
    }

    void Awake()
    {
        actorAnimator = ActorToMove.GetComponentInChildren<Animator>();    
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
