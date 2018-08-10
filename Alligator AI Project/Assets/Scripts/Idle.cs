using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Task
{
    public override Status Execute()
    {
        Debug.Log("Idle");
        return Status.Success;
    }
}
