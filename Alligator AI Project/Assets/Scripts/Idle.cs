using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Task
{
    float timeToIdle = 2f;

    public override Status Execute(GameObject actor, MovementController controller)
    {
        Debug.Log("Idle");

        timeToIdle -= Time.deltaTime;

        if (timeToIdle <= 0f)
        {
            return Status.Success;
        }

        controller.ActorAnimator.SetInteger("AnimState", 0);
        return Status.InProgress;
    }

    public override void Reset()
    {
        timeToIdle = 2f;
        base.Reset();
    }
}
