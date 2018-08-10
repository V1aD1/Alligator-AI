using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree {

    public Task Root;
    public Task CurrentNode;

    public BehaviourTree(Task root)
    {
        Root = root;
        CurrentNode = Root;
    }

    /*void Update()
    {
        Debug.Log("BehaviourTree update");
        var status = CurrentNode.Execute();

        if(status == Status.Success)
        {
            CurrentNode = Root;
        }
    }*/

}
