using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HaveIArrived : Node
{
    public HaveIArrived(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If initial target is zero, just succeed
        if (ownerTank.targetPos == Vector3.zero)
        {
            return NODE_STATUS.SUCCESS;
        }

        //If within arrival range, succeed
        float distance = ownerTank.navAgent.remainingDistance;
        if (distance < NeededVariables.ARRIVALRANGE)
        {
            return NODE_STATUS.SUCCESS;
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }


    }
}