using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HasNotChecked3Points : Node
{
    public HasNotChecked3Points(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If not been to 3 points in the scout location
        if (ownerTank.areaSearchNum < 2)
        {
            //Succeed
            return NODE_STATUS.SUCCESS;
        }
        //If have checked 3 positions and not found enemy
        else
        {
            //Set combat mode to false
            ownerTank.interactingEnemy = null;
            ownerTank.InCombatMode = false;
            ownerTank.areaSearchNum = 0;
            return NODE_STATUS.FAILURE;
        }


    }
}