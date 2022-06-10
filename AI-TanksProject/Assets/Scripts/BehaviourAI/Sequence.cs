using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sequence is basically the AND gate.
/// If any node in the sequence fails, it all fails
/// </summary>
public class Sequence : Composite
{
    //Tell base class the owner agent
    public Sequence(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        //Set returnStatus to fail
        NODE_STATUS returnStatus = NODE_STATUS.FAILURE;
        Node currentNode = childNodes[currentIndex];

        //If current node is not null
        if (currentNode != null)
        {
            //Call the current node update function and get its status
            NODE_STATUS currentNodeStatus = currentNode.Update();

            //Check if the current node has succeeded
            if (currentNodeStatus == NODE_STATUS.SUCCESS)
            {
                //Check if all the nodes have finished
                if (currentIndex == childNodes.Count - 1)
                {
                    //Set return status to success
                    returnStatus = NODE_STATUS.SUCCESS;
                }
                else
                {
                    //If all nodes have not finished, go to next node in list
                    currentIndex++;
                    //And set node status to running
                    returnStatus = NODE_STATUS.RUNNING;
                }
            }
            //If current node hasn't finished, 
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
