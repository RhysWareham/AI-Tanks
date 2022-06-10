using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReduceMyHealth : Node
{
    public ReduceMyHealth(Tank ownerAgent) : base(ownerAgent)
    {
    }

    public override NODE_STATUS Update()
    {
        if(ownerTank.takingDamageAmount != 0)
        {
            //Reduce health
            ownerTank.currentHealth -= ownerTank.takingDamageAmount;
            //Set damage amount to 0
            ownerTank.takingDamageAmount = 0;

            //Adjust health slider
            ownerTank.SetHealthUI();

            return NODE_STATUS.SUCCESS;
        }

        return NODE_STATUS.FAILURE;
    }


}
