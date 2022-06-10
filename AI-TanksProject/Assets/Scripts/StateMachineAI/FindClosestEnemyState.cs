using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemyState : State
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    public FindClosestEnemyState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.FINDCLOSESTENEMY;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered FindClosestEnemy state");

    }

    public override void Exit()
    {
        Debug.Log("Exiting FindClosestEnemy state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating FindClosestEnemy state");

        
        float distanceToTarget = float.MaxValue;
        Transform newTarget = null;

        
        foreach (Transform enemy in ownerTank.seenEnemies)
        {
            //If enemy is not this tank
            if (enemy != ownerTank.transform)
            {
                //Get distance of enemy
                float newDistance = Vector3.Distance(ownerTank.transform.position, enemy.transform.position);
                //If new distance is shorter than old distance to target
                if (newDistance < distanceToTarget)
                {
                    //
                    distanceToTarget = newDistance;
                    newTarget = enemy;
                }
            }
        }

        //If newTarget is not null
        if(newTarget != null)
        {
            //New closest enemy is New Target
            ownerTank.closestEnemy = newTarget;
            stateMachine.ChangeState(StateID.CHECKIFSPOTTED);


        }
        else
        {
            stateMachine.ChangeState(StateID.WANDER);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
