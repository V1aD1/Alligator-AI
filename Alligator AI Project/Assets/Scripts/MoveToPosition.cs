using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task changes the parameters of the movement script so that it 
/// </summary>
public class MoveToPosition : Task
{
    float maxTurnSpeed = 0.5f;
    float currentTurnSpeed = 0f;
    float currentMoveSpeed = 0f;

    public MoveToPosition(float actorForwardAxisLength)
    {
        
    }

    public override Status Execute(GameObject actor, MovementController controller)
    {
        //ignoring y component in case destination is above or below the actor
        //which in this roaming behavior doesn't account for
        Vector3 vectorToDestination = controller.Destination - actor.transform.position;
        vectorToDestination.y = 0;

        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, controller.MaxMovementSpeed, Time.deltaTime);
        currentTurnSpeed = Mathf.Lerp(currentTurnSpeed, maxTurnSpeed, Time.deltaTime);

        actor.transform.rotation = Quaternion.Slerp(
            actor.transform.rotation,
            Quaternion.LookRotation(controller.Destination - actor.transform.position),
            maxTurnSpeed * Time.deltaTime);
        actor.transform.position += actor.transform.forward * controller.MaxMovementSpeed * Time.deltaTime;

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
    // 1. Max turn speed = 1 degree/ second
    // 2. Speed / distance to destination ~= 0.63 
    // 3. Actor is heading in a perpendicular direction to the destination
    //
    //this math is not random! I inspected the orbiting distance at different speeds and reached the conclusions
    //stated above. Of course, if the Max Turn Speed is different than 1 degree/second, then the 
    //speed / distance to destination ratio will be different. BUT the max turn speed  is not exposed, since 
    //it would look unnatural if the alligator could turn faster. So I do not want to do the unnecessary calculations
    //every frame to determine the 
    private bool IsInOrbit(Vector3 vectorToDestination, float maxMovementSpeed, Vector3 actorForward)
    {
        //the ratio and angle are higher than the actual values as a safety factor
        if (vectorToDestination.magnitude < maxMovementSpeed * 0.7f && Vector3.Angle(vectorToDestination, actorForward) > 80f)
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
