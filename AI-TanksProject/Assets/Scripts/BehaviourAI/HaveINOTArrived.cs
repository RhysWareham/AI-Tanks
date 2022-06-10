using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HaveINOTArrived : Node
{
    public HaveINOTArrived(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If initial target is zero, just succeed
        if (ownerTank.targetPos != Vector3.zero)
        {
            ownerTank.navAgent.SetDestination(-ownerTank.targetPos);
            return NODE_STATUS.SUCCESS;
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }


    }
}