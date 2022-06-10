using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Selector is an OR gate
/// If this fails, try next, if that fails, try next, until no nodes are left.
/// If 1 succeedes, whole thing succeededs
/// </summary>
public class Selector : Composite
{
    public Selector(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //Set returnStatus to fail
        NODE_STATUS returnStatus = NODE_STATUS.FAILURE;
        Node currentNode = childNodes[currentIndex];

        if (currentNode != null)
        {
            //Call the current node update function and get its status
            NODE_STATUS currentNodeStatus = currentNode.Update();

            //Check if the current node has FAILED
            if (currentNodeStatus == NODE_STATUS.FAILURE)
            {
                //Check if on last node, meaning no other node to fall back on
                if (currentIndex == childNodes.Count - 1)
                {
                    //Then the whole selector has failed
                    returnStatus = NODE_STATUS.FAILURE;
                }
                else
                {
                    //If all nodes have not finished, go to next node in list
                    currentIndex++;
                    //And set node status to running
                    returnStatus = NODE_STATUS.RUNNING;
                }
            }
            //If current node has succeeded, 
            else
            {
                //Sequence mateches the nodes status
                returnStatus = currentNodeStatus;
            }
        }

        //If the sequence itself has succeeded or failed
        if (returnStatus == NODE_STATUS.SUCCESS || returnStatus == NODE_STATUS.FAILURE)
        {
            //Call reset function
            Reset();
        }

        //Return the status of the current node
        return returnStatus;
    }
}

