using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task changes the parameters of the movement script so that it 
/// </summary>
public class MoveToPosition : Task
{
    float maxTurnSpeed = 1f;
    float currentTurnSpeed = 0f;
    float currentMoveSpeed = 0f;
    float actorLength;

    public MoveToPosition(float actorForwardAxisLength)
    {
        actorLength = actorForwardAxisLength;
    }

    public override Status Execute(GameObject actor, MovementController controller)
    {
        //ignoring y component in case destination is above or below the actor
        //which in this roaming behavior doesn't account for
        Vector3 heading = controller.Destination - actor.transform.position;
        heading.y = 0;

        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, controller.MaxMovementSpeed, Time.deltaTime);
        currentTurnSpeed = Mathf.Lerp(currentTurnSpeed, maxTurnSpeed, Time.deltaTime);

        actor.transform.rotation = Quaternion.Slerp(
            actor.transform.rotation,
            Quaternion.LookRotation(controller.Destination - actor.transform.position),
            currentTurnSpeed * Time.deltaTime);
        actor.transform.position += actor.transform.forward * currentMoveSpeed * Time.deltaTime;

        controller.ActorAnimator.SetInteger("AnimState", 1);

        Debug.DrawLine(actor.transform.position, actor.transform.forward * 100, Color.blue);
        if (heading.magnitude <= controller.MinDistanceUntilDestinationReached)
        {
            return Status.Success;
        }

        //the actor will circle around the destination endlessly if it's too close to where
        //they were previously idling. In order to avoid this,
        //we assume the destination has been reached if the destination 
        //is close to the actor, but at a near 90 degree angle to their forward vector
        //(though this math will only work for symmetrical animals)
        if (heading.magnitude < actorLength/2f && Vector3.Angle(heading, actor.transform.forward) > 80f)
        {
            return Status.Fail;
        }

        return Status.InProgress;
    }

    public override void Reset()
    {
        currentTurnSpeed = 0f;
        currentMoveSpeed = 0f;
        base.Reset();
    }
}
