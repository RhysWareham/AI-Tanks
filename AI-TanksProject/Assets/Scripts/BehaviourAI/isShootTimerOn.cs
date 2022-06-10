using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsShootTimerOn : Node
{
    
    public IsShootTimerOn(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        //If timer is more than 0
        if ((ownerTank.shootTimer -= Time.deltaTime) >= 0.0f)
        {
            //Timer is still on

            return NODE_STATUS.SUCCESS;
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }

    }
}

