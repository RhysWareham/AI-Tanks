using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IGotSafeTarget : Node
{
    public IGotSafeTarget(Tank ownerAgent) : base(ownerAgent)
    {
    }

    public override NODE_STATUS Update()
    {
        if (ownerTank.safeTargetPos != Vector3.zero)
        {
            return NODE_STATUS.SUCCESS;
        }
        return NODE_STATUS.FAILURE;
    }


}
