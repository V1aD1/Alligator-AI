using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base type for every node in the behaviour tree.
/// Must implement an Execute() function.
/// </summary>
public abstract class Task {

    public Status Status;
    public string Name;

    public abstract Status Execute(GameObject actor, MovementController controller);
}


public enum Status
{
    Success = 1,
    Fail = 2,
    InProgress = 3
}