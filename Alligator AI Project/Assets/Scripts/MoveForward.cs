using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float MoveSpeed = 10f;
    public Vector3 MovementDirection = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(MovementDirection * MoveSpeed * Time.deltaTime);
        Debug.DrawLine(transform.position, MovementDirection * 100f, Color.red);
    }
}

