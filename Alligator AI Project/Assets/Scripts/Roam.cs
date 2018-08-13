using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : Sequencer {

    public Roam(Vector3 minRange, Vector3 maxRange, float actorForwardAxisLength)
    {
        Children = new Task[2];
        Children[0] = new ChooseAndSetNewDestination(minRange, maxRange);
        Children[1] = new MoveToPosition(actorForwardAxisLength);

    }
}
