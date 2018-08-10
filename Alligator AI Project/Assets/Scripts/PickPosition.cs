using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPosition : Task
{
    public Vector3 MinRange;
    public Vector3 MaxRange;

    public PickPosition(Vector3 minRange, Vector3 maxRange)
    {
        MinRange = minRange;
        MaxRange = maxRange;
    }

    public override Status Execute()
    {
        Debug.Log("PickPosition picked position: ");
        Debug.Log(new Vector3(Random.Range(MinRange.x, MaxRange.x),
                              Random.Range(MinRange.y, MaxRange.y),
                              Random.Range(MinRange.z, MaxRange.z)));
        return Status.Success;
    }
}
