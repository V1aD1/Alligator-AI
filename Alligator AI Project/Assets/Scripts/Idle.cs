using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Task
{
    float maxTimeToIdle;
    float timeToIdle = 0f;

    public Idle(float maxTimeToIdle)
    {
        this.maxTimeToIdle = maxTimeToIdle;
        timeToIdle = Random.Range(0f, maxTimeToIdle);
    }

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
        timeToIdle = Random.Range(0f, maxTimeToIdle);
        base.Reset();
    }
}
