using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionSuperState : State
{
    public CautionSuperState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override StateID GetID()
    {
        return base.GetID();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        //If know enemy's position
        //Rotate turret towards Enemy
        if (ownerTank.canSeeEnemy)
        {
            AimAtEnemy();
            
        }
 


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    //Aim turret at enemy
    public void AimAtEnemy()
    {
        //Get target direction
        Vector3 targetDirNorm = (ownerTank.closestEnemy.position - ownerTank.transform.position).normalized;
        //Get new look rotation
        Quaternion lookRotation = Quaternion.LookRotation(targetDirNorm, ownerTank.transform.up);
        lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y, 0);

        //If turret is pointing outside the aim precision
        if (Vector3.Angle(ownerTank.tankTurret.transform.forward, targetDirNorm) > NeededVariables.AIMLEEWAY)
        {
            //Rotate turret towards enemy
            ownerTank.tankTurret.transform.rotation = Quaternion.RotateTowards(ownerTank.tankTurret.transform.rotation, lookRotation, Time.deltaTime * NeededVariables.TURRETAIMSPEED);
            ownerTank.aimingAtTarget = false;
        }
        else
        {
            ownerTank.aimingAtTarget = true;
        }
        
    }
}
