using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task changes the parameters of the movement script so that it 
/// </summary>
public class MoveToPosition : Task
{
    Vector3 currentDirection = Vector3.zero;
    float currentSpeed = 0f;

    public override Status Execute(GameObject actor, MovementController controller)
    {
        currentDirection = actor.transform.forward;
        Vector3 heading = controller.Destination - actor.transform.position;

        //it is assumed that this script will be used in environments
        //with gravity. This step is done
        //so that the actor doesn't move upwards
        heading.y = 0;

        Vector3 idealDirection = heading / heading.magnitude;


        currentDirection = Vector3.Lerp(currentDirection, idealDirection, Time.deltaTime);
        currentSpeed = Mathf.Lerp(currentSpeed, controller.MaxSpeed, Time.deltaTime);
        //actor.transform.rotation = Quaternion.FromToRotation(Vector3.forward, idealDirection);

        actor.transform.Translate(idealDirection * currentSpeed * Time.deltaTime);

        Debug.DrawLine(actor.transform.position, currentDirection * 100, Color.blue);
        Debug.DrawLine(actor.transform.position, idealDirection * 100, Color.white);

        controller.ActorAnimator.SetInteger("AnimState", 1);

        if (heading.magnitude <= controller.MinDistanceUntilDestinationReached)
        {
            return Status.Success;
        }

        return Status.InProgress;
    }

    public override void Reset()
    {
        currentSpeed = 0f;
        base.Reset();
    }
}
