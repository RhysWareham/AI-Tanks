using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NODE_STATUS
{
    SUCCESS,
    FAILURE,
    READY,
    RUNNING,
    NONE
}

public abstract class Node
{
    public Tank ownerTank { get; private set; }

    public Node(Tank ownerTank)
    {
        //Store a reference of agent who owns this behaviour tree
        this.ownerTank = ownerTank;
    }

    public virtual NODE_STATUS Update()
    {
        return NODE_STATUS.NONE;
    }
}
