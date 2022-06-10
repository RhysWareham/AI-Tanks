using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : CautionSuperState
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    private float h = 2;
    private float gravity = -18;

    public ShootState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.SHOOT;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered ShootState state");

    }

    public override void Exit()
    {
        Debug.Log("Exiting ShootState state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating ShootState state");

        if (ownerTank.closestEnemy == null)
        {
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }

        //If aiming at target and enemy is visible
        if (ownerTank.aimingAtTarget && ownerTank.seenEnemies.Count > 0)
        {
            //If timer is less than or equal to 0, allowed to shoot
            if(ownerTank.shootTimer <= 0)
            {
                //Calculate velocity and fire
                Vector3 velocity = CalculateShellVelocity();
                Fire(velocity);
                ownerTank.shootTimer = NeededVariables.SHOOTTIMER;
                //Once shot, runaway

            }
            stateMachine.ChangeState(StateID.RUNAWAY);

        }
        //If can't see enemy or not aiming at target
        else
        {
            stateMachine.ChangeState(StateID.CHECKIFINRANGE);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    //Function which returns the velocity that the shell should spawn at
    private Vector3 CalculateShellVelocity()
    {
        float displacementY = ownerTank.closestEnemy.position.y - ownerTank.firePoint.position.y;
        Vector3 dispacementXZ = new Vector3(ownerTank.closestEnemy.position.x - ownerTank.firePoint.position.x, 0, ownerTank.closestEnemy.position.z - ownerTank.firePoint.position.z);

        Vector3 velY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velXZ = dispacementXZ / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity));

        return velXZ + velY;
    }

    //Function to instantiate a shell at firepoint with velocity
    private void Fire(Vector3 vel)
    {
        //Set gravity to our gravity
        Physics.gravity = Vector3.up * gravity;

        GameObject shellInstance;
        shellInstance = GameObject.Instantiate(ownerTank.shellPrefab, ownerTank.firePoint.position, ownerTank.firePoint.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = vel;
        Debug.Log("fired shell");
    }
}
