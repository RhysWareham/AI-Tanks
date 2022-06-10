using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    public IdleState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.IDLE;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered IDLE state");

    }

    public override void Exit()
    {
        Debug.Log("Exiting IDLE state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating IDLE state");


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
