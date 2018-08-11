using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task chooses a random point between the specified ranges
/// and sets the destination in the corresponding MovementController.
/// The y component of the destination
/// will always be 0, since it is assumed this task is ONLY
/// used for grounded actors. 
/// </summary>
public class ChooseAndSetNewDestination : Task
{
    Vector2 MinRange;
    Vector2 MaxRange;

    public ChooseAndSetNewDestination(Vector2 minRange, Vector2 maxRange)
    {
        MinRange = minRange;
        MaxRange = maxRange;
    }

    public override Status Execute(GameObject actor, MovementController controller)
    {
        Vector3 newPos = new Vector3(Random.Range(MinRange.x, MaxRange.x),
                              0,
                              Random.Range(MinRange.y, MaxRange.y));

        
        controller.Destination = newPos;

        return Status.Success;
    }
}
