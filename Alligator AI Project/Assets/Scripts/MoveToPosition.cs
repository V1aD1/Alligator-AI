using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task changes the parameters of the movement script so that it 
/// </summary>
public class MoveToPosition : Task
{
    public override Status Execute()
    {
        Debug.Log("MoveToPosition");
        return Status.Success;
    }
}
