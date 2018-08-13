using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    [Tooltip("Recommended below 3. Otherwise actors the size of alligators " +
             "will \"orbit\" and never reach destination")]
    public float MaxMovementSpeed = 1f;
    public float MaxSecondsToIdle = 4f;
    [Tooltip("Actor has arrived if they are within this distance from their destination")]
    public float MinDistanceUntilDestinationReached = 0.1f;
    [Tooltip("Corner of walkable area")]
    public Vector2 MinRange = new Vector2(4, -3);
    [Tooltip("OPPOSITE corner of walkable area")]
    public Vector2 MaxRange = new Vector2(-4, 4);
    //public GameObject ActorToMove;

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
        ActorAnimator = gameObject.GetComponent<Animator>();    
    }

    // Use this for initialization
    void Start () {
        float actorForwardAxisLength = gameObject.GetComponent<BoxCollider>().bounds.size.z;

        //the 4* multiplier is abit arbitrary
        float recommendedAreaDiagonalLength = 4 * actorForwardAxisLength;

        Debug.Assert(ActorAnimator != null, "ActorAnimator not set!");
        Debug.Assert((MinRange - MaxRange).magnitude > recommendedAreaDiagonalLength, 
                     "It is recommended that the distance between the corners of the walkable area is at least: "
                     +recommendedAreaDiagonalLength + "so as to avoid strange behaviour.");

        //Setup the behaviour tree
        var root = new Sequencer();
        root.Children = new Task[2];
        root.Children[0] = new Roam(MinRange, MaxRange);
        root.Children[1] = new Idle(MaxSecondsToIdle);

        Root = root;
    }
	
	// Update is called once per frame
	void Update () {
        Root.Execute(gameObject, this);
        Debug.DrawLine(gameObject.transform.position, destination, Color.red);
	}
}
