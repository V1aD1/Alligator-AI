using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task changes the parameters of the movement script so that it 
/// </summary>
public class MoveToPosition : Task
{
    float turnSpeed = 1f;
    float currentSpeed = 0f;

    public override Status Execute(GameObject actor, MovementController controller)
    {
        //ignoring y component in case destination is too far above or below the actor
        Vector3 heading = controller.Destination - actor.transform.position;
        heading.y = 0;

        actor.transform.rotation = Quaternion.Slerp(
            actor.transform.rotation,
            Quaternion.LookRotation(controller.Destination - actor.transform.position),
            turnSpeed * Time.deltaTime);

        currentSpeed = Mathf.Lerp(currentSpeed, controller.MaxSpeed, Time.deltaTime);
        actor.transform.position += actor.transform.forward * currentSpeed * Time.deltaTime;

        controller.ActorAnimator.SetInteger("AnimState", 1);

        Debug.DrawLine(actor.transform.position, actor.transform.forward * 100, Color.blue);
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
