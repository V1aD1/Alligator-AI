using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task changes the parameters of the movement script so that it 
/// </summary>
public class MoveToPosition : Task
{
    Vector3 currentDirection = Vector3.zero;

    public override Status Execute(GameObject actor, MovementController controller)
    {
        currentDirection = actor.transform.forward;
        Vector3 heading = controller.Destination - actor.transform.position;
        Vector3 idealDirection = heading / heading.magnitude;


        currentDirection = Vector3.Lerp(currentDirection, idealDirection, Time.deltaTime);
        actor.transform.rotation = Quaternion.FromToRotation(Vector3.forward, currentDirection);

        actor.transform.Translate(currentDirection * 1 * Time.deltaTime);

        Debug.DrawLine(actor.transform.position, currentDirection * 100, Color.blue);
        Debug.DrawLine(actor.transform.position, idealDirection * 100, Color.white);
        return Status.InProgress;
    }
}
