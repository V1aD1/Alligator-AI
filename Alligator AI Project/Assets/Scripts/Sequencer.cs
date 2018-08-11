using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This task runs all its children in the order they are stored in the Children array.
/// </summary>
public class Sequencer : Task
{

    public Task[] Children;

    int currentLeafId = 0;

    /*public Sequencer(Task[] children)
    {
        if (children.Length == 0)
            Debug.LogError("Sequencer was created with no children");

        currentLeafId = 0;
    }*/

    public override Status Execute(GameObject actor, MovementController controller)
    {
        var status = Children[currentLeafId].Execute(actor, controller);

        if (status == Status.InProgress)
        {
            return Status.InProgress;
        }

        else if (status == Status.Fail)
        {
            currentLeafId = 0;
            return Status.Fail;
        }

        //the current child task succesfully completed
        currentLeafId++;

        if (currentLeafId >= Children.Length)
        {
            currentLeafId = 0;
            return Status.Success;
        }

        return Status.InProgress;
    }
}
