using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSequenceNode : Node
{
    public TestSequenceNode(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        Debug.Log("In Sequence");
        return NODE_STATUS.SUCCESS;

    }
}

