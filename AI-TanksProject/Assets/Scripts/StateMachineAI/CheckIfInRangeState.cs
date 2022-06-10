using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfInRangeState : CautionSuperState
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    public CheckIfInRangeState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.CHECKIFINRANGE;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered CheckIfInRange state");
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting CheckIfInRange state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating CheckIfInRange state");
        if (ownerTank.closestEnemy == null)
        {
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }

        float distance = Vector3.Distance(ownerTank.transform.position, ownerTank.closestEnemy.position);

        //If enemy distance is less than shoot range
        if( distance < NeededVariables.SHOOTRANGE)
        {
            //but more than safe distance and aiming at target
            if (distance > NeededVariables.SAFESHOOTRANGE)
            {
                if(/*ownerTank.aimingAtTarget && */ownerTank.seenEnemies.Count > 0)
                {
                    //Shoot
                    stateMachine.ChangeState(StateID.SHOOT);

                }
                //If can't see enemy
                else if(ownerTank.seenEnemies.Count == 0)
                {
                    stateMachine.ChangeState(StateID.CHECKIFSPOTTED);
                }

            }
            //If enemy is closer than safe distance
            else
            {
                //Run away to cover
                //Run away
                stateMachine.ChangeState(StateID.RUNAWAY);
            }
        }
        //If not in range
        else
        {
            //And can see enemy
            if(ownerTank.seenEnemies.Count > 0)
            {
                //Get closer position, if cover is available, go there
                stateMachine.ChangeState(StateID.FINDCLOSERPOS);

            }
            else
            {
                //Check if enemy can see me
                stateMachine.ChangeState(StateID.CHECKIFSPOTTED);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
