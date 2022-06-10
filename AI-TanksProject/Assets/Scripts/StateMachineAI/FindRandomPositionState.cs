using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRandomPositionState : State
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    public FindRandomPositionState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.FINDRANDOMPOS;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered FindRandomPos state");

    }

    public override void Exit()
    {
        Debug.Log("Exiting RandomPos state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating FindRandomPos state");

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
            stateMachine.ChangeState(StateID.WANDER);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
