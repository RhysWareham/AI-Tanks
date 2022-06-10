using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BehaviourTree : MonoBehaviour
{
    //Store agent who owns this tree
    protected Tank ownerTank { get; private set; }
    //Store the root node
    public Node rootNode { get; protected set; }

    public BehaviourTree(Tank owner)
    {
        ownerTank = owner;
    }

    // Update is called once per frame
    public void Update()
    {
        rootNode.Update();
    }
}
