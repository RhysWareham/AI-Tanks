using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetLastKnownPos : Node
{
    public GetLastKnownPos(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If lastknownpos is not null
        if(ownerTank.lastKnownEnemyPos != null)
        {
            ownerTank.targetPos = ownerTank.lastKnownEnemyPos.position;
            return NODE_STATUS.SUCCESS;
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }

        


    }
}