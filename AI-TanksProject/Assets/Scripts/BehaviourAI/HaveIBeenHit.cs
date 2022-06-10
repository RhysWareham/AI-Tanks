using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HaveIBeenHit : Node
{
    public HaveIBeenHit(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If in combat mode
        if (ownerTank.takingDamageAmount > 0)
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