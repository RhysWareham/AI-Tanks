using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsEnemyInRange : Node
{
    public IsEnemyInRange(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        if(ownerTank.targetPos == Vector3.zero)
        {
            return NODE_STATUS.FAILURE;
        }
        else
        {
            //If remaining distance from enemy is less than the shoot range and more than minimum range (to avoid injury)
            float distance = NeededVariables.RemainingDistance(ownerTank.navAgent.path.corners);
            
            if( distance < NeededVariables.SHOOTRANGE)
            {
                Debug.Log("IsInRange");
                return NODE_STATUS.SUCCESS;
            }
            else
            {
                return NODE_STATUS.FAILURE;
            }
        }

        

    }
}

