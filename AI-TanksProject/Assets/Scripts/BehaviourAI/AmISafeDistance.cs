using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmISafeDistance : Node
{
    public AmISafeDistance(Tank ownerTank) : base(ownerTank)
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
            //If remaining distance from enemy is more than minimum range (to avoid injury)
            float distance = Vector3.Distance(ownerTank.transform.position, ownerTank.targetPos);
            if( NeededVariables.SAFESHOOTRANGE < distance)
            {
                Debug.Log("SafeDistance");
                ownerTank.safeTargetPos = Vector3.zero;
                return NODE_STATUS.SUCCESS;
            }
            else
            {
                return NODE_STATUS.FAILURE;
            }
        }

        

    }
}

