using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float MoveSpeed = 10f;
    public Vector3 Destination = Vector3.zero;
    public float CloseEnough = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = Destination - transform.position;
        heading.y = 0;
        Vector3 idealDirection = heading / heading.magnitude;

        //transform.Translate(transform.forward * MoveSpeed * Time.deltaTime);
        if (heading.magnitude > CloseEnough)
        {
            //transform.rotation = Quaternion.FromToRotation(Vector3.forward, idealDirection);
            transform.Translate(idealDirection * MoveSpeed * Time.deltaTime);
        }
        Debug.DrawLine(transform.position, Destination, Color.red);
        Debug.DrawLine(transform.position, transform.forward* 100, Color.blue);
        Debug.DrawLine(transform.position, idealDirection * 100, Color.white);

    }
}

