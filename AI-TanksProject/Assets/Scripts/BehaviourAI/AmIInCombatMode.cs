using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmIInCombatMode : Node
{
    public AmIInCombatMode(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If in combat mode
        if (ownerTank.InCombatMode)
        {
            if (ownerTank.shootTimer >= 0 && ownerTank.canShoot == false)
            {
                ownerTank.shootTimer -= Time.deltaTime;
            }
            else
            {
                ownerTank.canShoot = true;
            }
            //Succeed
            return NODE_STATUS.SUCCESS;
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }


    }
}