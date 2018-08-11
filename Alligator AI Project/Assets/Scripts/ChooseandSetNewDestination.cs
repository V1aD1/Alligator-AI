using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseandSetNewDestination : Task
{
    public Vector3 MinRange;
    public Vector3 MaxRange;

    public ChooseandSetNewDestination(Vector3 minRange, Vector3 maxRange)
    {
        MinRange = minRange;
        MaxRange = maxRange;
    }

    public override Status Execute(GameObject actor, MovementController controller)
    {
        Vector3 newPos = new Vector3(Random.Range(MinRange.x, MaxRange.x),
                              Random.Range(MinRange.y, MaxRange.y),
                              Random.Range(MinRange.z, MaxRange.z));

        
        controller.Destination = newPos;

        return Status.Success;
    }
}
