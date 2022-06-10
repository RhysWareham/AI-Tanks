using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FindNearSafePosition : Node
{
    public FindNearSafePosition(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If lastknown pos is not 0,0,0
        
        if(ownerTank.lastKnownEnemyPos != null)
        {
            //Get a random position within scout radius
            Vector3 randomPos = ownerTank.lastKnownEnemyPos.position + (Random.insideUnitSphere * NeededVariables.SHOOTRANGE);
            //While the distance between randomPos and enemy is less than the minimumShootRange
            while(Vector3.Distance(randomPos, ownerTank.lastKnownEnemyPos.position) < NeededVariables.SAFESHOOTRANGE + 3f)
            {
                randomPos = ownerTank.lastKnownEnemyPos.position + (Random.insideUnitSphere * NeededVariables.SHOOTRANGE);
            }

            //Set position on nav mesh
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, NeededVariables.BORDERDISTANCE, UnityEngine.AI.NavMesh.AllAreas);
            
            if(hit.position != Vector3.zero)
            {
                ownerTank.safeTargetPos = hit.position;
                return NODE_STATUS.SUCCESS;
            }
            else
            {
                return NODE_STATUS.FAILURE;
            }
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }


    }
}