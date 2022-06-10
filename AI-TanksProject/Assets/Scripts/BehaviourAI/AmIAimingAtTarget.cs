using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmIAimingAtTarget : Node
{
    public AmIAimingAtTarget(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If in combat mode
        if (ownerTank.aimingAtTarget)
        {
            //Succeed
            return NODE_STATUS.SUCCESS;
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }


    }
}