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
    float orbitRatio;

    public MoveToPosition()
    {
        //the actual ratio is more like 0.65f/max turn speed, 
        //but we use 0.8f/max turn speed because it's safer
        orbitRatio = 0.8f / maxTurnSpeed;
    }

    public override Status Execute(GameObject actor, MovementController controller)
    {
        //ignoring y component in case destination is above or below the actor
        //which this roaming behavior doesn't account for
        Vector3 vectorToDestination = controller.Destination - actor.transform.position;
        vectorToDestination.y = 0;

        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, controller.MaxMovementSpeed, Time.deltaTime);
        currentTurnSpeed = Mathf.Lerp(currentTurnSpeed, maxTurnSpeed, Time.deltaTime);

        actor.transform.rotation = Quaternion.Slerp(
            actor.transform.rotation,
            Quaternion.LookRotation(controller.Destination - actor.transform.position),
            currentTurnSpeed * Time.deltaTime);
        actor.transform.position += actor.transform.forward * currentMoveSpeed * Time.deltaTime;

        controller.ActorAnimator.SetInteger("AnimState", 1);

        Debug.DrawLine(actor.transform.position, actor.transform.forward * 100, Color.blue);
        if (vectorToDestination.magnitude <= controller.MinDistanceUntilDestinationReached)
        {
            return Status.Success;
        }

        //if the actor is in orbit around the destination it will never reach it,
        //so this task will never complete and has therefore failed
        if (IsInOrbit(vectorToDestination, controller.MaxMovementSpeed, actor.transform.forward))
        {
            return Status.Fail;
        }

        return Status.InProgress;
    }

    //the actor will orbit around the destination if: 
    // 1. Distance to destination / max movement speed ~= 0.65f/max turn speed 
    // 2. Actor is heading in a perpendicular direction to the destination
    //
    //this math is not random! I inspected the orbiting distance at different speeds and
    //turn angles to reach the conclusions stated above 
    private bool IsInOrbit(Vector3 vectorToDestination, float maxMovementSpeed, Vector3 actorForward)
    {
        //the ratio and angle are higher than the actual values as a safety factor
        if (vectorToDestination.magnitude / maxMovementSpeed < orbitRatio && Vector3.Angle(vectorToDestination, actorForward) > 80f)
        {
            return true;
        }

        return false;
    }

    public override void Reset()
    {
        currentTurnSpeed = 0f;
        currentMoveSpeed = 0f;
        base.Reset();
    }
}
