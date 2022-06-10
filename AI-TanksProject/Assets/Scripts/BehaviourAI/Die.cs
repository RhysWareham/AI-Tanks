using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Die : Node
{
    public Die(Tank ownerAgent) : base(ownerAgent)
    {
    }

    public override NODE_STATUS Update()
    {
        if(!ownerTank.isDead)
        {
            //Call OnDeath function
            ownerTank.OnDeath();
            return NODE_STATUS.SUCCESS;

        }

        return NODE_STATUS.FAILURE;
    }

}
