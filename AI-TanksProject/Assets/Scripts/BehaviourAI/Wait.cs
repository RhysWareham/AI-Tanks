using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wait : Node
{
    float waitTimer = NeededVariables.WAITTIME;
    public Wait(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        if ((waitTimer -= Time.deltaTime) <= 0.0f)
        {
            waitTimer = NeededVariables.WAITTIME;

            return NODE_STATUS.SUCCESS;
        }
        else
        {
            return NODE_STATUS.RUNNING;
        }

    }
}

