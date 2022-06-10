using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShouldIWait : Node
{
    public ShouldIWait(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        if(ownerTank.seenEnemies != null)
        {
            if(ownerTank.seenEnemies.Count == 0)
            {
                return NODE_STATUS.FAILURE;
            }
            else
            {
                return NODE_STATUS.SUCCESS;
            }

        }
        else
        {
            return NODE_STATUS.FAILURE;
        }

    }
}

