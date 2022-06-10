using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectPosInSearchArea : Node
{
    public SelectPosInSearchArea(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        //If lastknown pos is not 0,0,0
        Debug.Log("Dies Here");
        if(ownerTank.lastKnownEnemyPos != null)
        {
            //Get a random position within scout radius
            Vector3 randomPos = ownerTank.lastKnownEnemyPos.position + (Random.insideUnitSphere * ownerTank.scoutRadius);

            //Set position on nav mesh
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, NeededVariables.BORDERDISTANCE, UnityEngine.AI.NavMesh.AllAreas);
            
            if(hit.position != Vector3.zero)
            {
                ownerTank.targetPos = hit.position;
                ownerTank.areaSearchNum++;
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