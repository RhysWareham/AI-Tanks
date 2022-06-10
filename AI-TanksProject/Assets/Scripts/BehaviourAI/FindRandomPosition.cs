using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FindRandomPosition : Node
{
    public FindRandomPosition(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        //Random.insideUnitSphere finds a point within a radius of 1
        Vector3 randomPos = (Random.insideUnitSphere * NeededVariables.BORDERDISTANCE);

        //Set position on nav mesh
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, NeededVariables.BORDERDISTANCE, UnityEngine.AI.NavMesh.AllAreas);

        //If position is not zero/null
        if (hit.position != Vector3.zero)
        {
            //Set agent new targetPosition to the navmesh pos
            ownerTank.targetPos = hit.position;
            //Set status to Success
            return NODE_STATUS.SUCCESS;
        }
        //If no position has been found
        else
        {
            //Set status to failure
            return NODE_STATUS.FAILURE;
        }



    }
}
