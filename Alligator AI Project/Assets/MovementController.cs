using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float moveSpeed = 10f;

    Vector3 movementDirection;

	// Use this for initialization
	void Start () {
        movementDirection = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        Debug.DrawLine(transform.position, movementDirection * 100f, Color.red);
	}
}
