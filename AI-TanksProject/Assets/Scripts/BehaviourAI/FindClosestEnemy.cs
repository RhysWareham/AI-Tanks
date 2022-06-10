using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FindClosestEnemy : Node
{
    public FindClosestEnemy(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        float distanceToTarget = float.MaxValue;
        Transform newTarget = null;

        
        foreach(Transform enemy in ownerTank.seenEnemies)
        {
            //If enemy is not this tank
            if(enemy != ownerTank.transform)
            {
                float newDistance = Vector3.Distance(ownerTank.transform.position, enemy.transform.position);
                if(newDistance < distanceToTarget)
                {
                    distanceToTarget = newDistance;
                    newTarget = enemy;
                }
            }
        }

        if(newTarget != null)
        {
            ownerTank.targetPos = newTarget.position;
            ownerTank.interactingEnemy = newTarget.GetComponent<Tank>();
            ownerTank.lastKnownEnemyPos = newTarget;
            return NODE_STATUS.SUCCESS;
        }
        else
        {
            ownerTank.navAgent.isStopped = true;
            return NODE_STATUS.FAILURE; //This was returning RUNNING, which was leading to tanks stopping completely
        }

    }
}
