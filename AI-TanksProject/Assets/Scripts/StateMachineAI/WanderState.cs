using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    public WanderState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.WANDER;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        Debug.Log("Entering WanderState");
        base.Enter();

        ownerTank.navAgent.speed = NeededVariables.WANDERSPEED;

        //If no target position
        if(ownerTank.targetPos == Vector3.zero)
        {
            //Change to find random position state
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }
        
        ownerTank.navAgent.isStopped = false;

    }

    public override void Exit()
    {
        Debug.Log("Exiting WanderState");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        Debug.Log("Updating WanderState");
        base.LogicUpdate();

        //If seen an enemy
        if(ownerTank.seenEnemies != null)
        {
            if(ownerTank.seenEnemies.Count > 0)
            {
                ownerTank.canSeeEnemy = true;
                //If seen an enemy, get their position in FINDCLOSESTENEMY STATE
                stateMachine.ChangeState(StateID.FINDCLOSESTENEMY);

            }
        }

        ownerTank.navAgent.SetDestination(ownerTank.targetPos);

        //If within arrival range, succeed
        float distance = ownerTank.navAgent.remainingDistance;
        if (distance < NeededVariables.ARRIVALRANGE)
        {
            //Set target to 0
            ownerTank.targetPos = Vector3.zero;

            //Find new random position
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
