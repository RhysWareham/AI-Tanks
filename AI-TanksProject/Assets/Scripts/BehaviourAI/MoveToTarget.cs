using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveToTarget : Node
{
    public MoveToTarget(Tank ownerAgent) : base(ownerAgent)
    {
    }

    public override NODE_STATUS Update()
    {
        if (ownerTank.targetPos != Vector3.zero)
        {
            ownerTank.navAgent.isStopped = false;

            

            //Set speed depending on sneaking or charging at enemy?

            //Set destination
            ownerTank.navAgent.SetDestination(ownerTank.targetPos);

            return NODE_STATUS.SUCCESS;

        }

        return NODE_STATUS.FAILURE;
    }

}
