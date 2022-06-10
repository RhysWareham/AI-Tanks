using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : CautionSuperState
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    private float h = 2;
    private float gravity = -18;

    public DeathState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.DEATH;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Death state");

    }

    public override void Exit()
    {
        Debug.Log("Exiting Death state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating Death state");

        if(!ownerTank.isDead)
        {
            stateMachine.ChangeState(StateID.WANDER);
            ownerTank.OnDeath();

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
