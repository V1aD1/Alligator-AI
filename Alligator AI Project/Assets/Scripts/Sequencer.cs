using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task runs all its children in the order they are stored in the Children array.
/// </summary>
public class Sequencer : Task
{

    public Task[] Children;

    int currentTaskId = 0;

    /*public Sequencer(Task[] children)
    {
        if (children.Length == 0)
            Debug.LogError("Sequencer was created with no children");

        currentTaskId = 0;
    }*/

    public override Status Execute(GameObject actor, MovementController controller)
    {
        var status = Children[currentTaskId].Execute(actor, controller);

        if (status == Status.InProgress)
        {
            return Status.InProgress;
        }

        else if (status == Status.Fail)
        {
            currentTaskId = 0;
            return Status.Fail;
        }

        //the current child task succesfully completed
        Children[currentTaskId].Reset();
        currentTaskId++;

        if (currentTaskId >= Children.Length)
        {
            currentTaskId = 0;
            return Status.Success;
        }

        return Status.InProgress;
    }
}
