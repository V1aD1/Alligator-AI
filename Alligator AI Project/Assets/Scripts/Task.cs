using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task {

    public Status Status;
    public string Name;

    public abstract Status Execute();
}


public enum Status
{
    Success = 1,
    Fail = 2,
    InProgress = 3
}