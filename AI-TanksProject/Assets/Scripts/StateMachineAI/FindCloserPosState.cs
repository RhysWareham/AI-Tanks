using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCloserPosState : CautionSuperState
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    public FindCloserPosState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.FINDCLOSERPOS;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered FindCloserPosState state");

    }

    public override void Exit()
    {
        Debug.Log("Exiting FindCloserPosState state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating FindCloserPosState state");

        if (ownerTank.closestEnemy == null)
        {
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }

        //If not close enough to shoot yet
        if (Vector3.Distance(ownerTank.transform.position, ownerTank.closestEnemy.position) > NeededVariables.SHOOTRANGE)
        {
            ownerTank.navAgent.SetDestination(ownerTank.closestEnemy.position);
        }
        else
        {
            //If close enough, checkRange state
            stateMachine.ChangeState(StateID.CHECKIFINRANGE);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
