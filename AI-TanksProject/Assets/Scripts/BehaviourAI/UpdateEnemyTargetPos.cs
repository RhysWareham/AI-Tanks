using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateEnemyTargetPos : Node
{
    public UpdateEnemyTargetPos(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        if(ownerTank.interactingEnemy != null)
        {
            if(ownerTank.seenEnemies.Count == 0)
            {
                
                return NODE_STATUS.FAILURE;
            }
            else
            {
                ownerTank.targetPos = ownerTank.interactingEnemy.transform.position;
                ownerTank.lastKnownEnemyPos = ownerTank.interactingEnemy.transform;
                return NODE_STATUS.SUCCESS;
            }

        }
        else
        {
            return NODE_STATUS.FAILURE;
        }

    }
}

