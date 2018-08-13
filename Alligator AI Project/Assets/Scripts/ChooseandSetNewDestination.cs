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
    Vector2 minRange;
    Vector2 maxRange;

    public ChooseAndSetNewDestination(Vector2 minRange, Vector2 maxRange)
    {
        this.minRange = minRange;
        this.maxRange = maxRange;
    }

    public override Status Execute(GameObject actor, MovementController controller)
    {
        Vector3 newPos = new Vector3(Random.Range(minRange.x, maxRange.x),
                              0,
                              Random.Range(minRange.y, maxRange.y));

        controller.Destination = newPos;
        return Status.Success;
    }
}
