using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanISeeEnemy : Node
{
    public CanISeeEnemy(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        if(ownerTank.seenEnemies != null)
        {
            


            if (ownerTank.seenEnemies.Count == 0)
            {
                //ownerTank.aimingAtTarget = false;
                return NODE_STATUS.FAILURE;
            }
            else
            {
                
                return NODE_STATUS.SUCCESS;
            }

        }
        else
        {
            //ownerTank.aimingAtTarget = false;
            return NODE_STATUS.FAILURE;
        }

    }
}

