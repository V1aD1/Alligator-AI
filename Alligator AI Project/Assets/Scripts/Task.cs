using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base type for every node in the behaviour tree.
/// Must implement Execute() and Reset() functions.
/// </summary>
public abstract class Task {

    public Status Status;
    public string Name;

    public abstract Status Execute(GameObject actor, MovementController controller);
    public virtual void Reset()
    {
        ;
    }
}

public enum Status
{
    Success = 1,
    Fail = 2,
    InProgress = 3
}