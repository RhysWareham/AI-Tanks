using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmIDead : Node
{
    public AmIDead(Tank ownerAgent) : base(ownerAgent)
    {
    }

    public override NODE_STATUS Update()
    {
        //If health is 0
        if (ownerTank.currentHealth <= 0)
        {
            

            return NODE_STATUS.SUCCESS;

        }

        return NODE_STATUS.FAILURE;
    }

}
