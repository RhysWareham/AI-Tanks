using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfSpottedState : CautionSuperState
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    public CheckIfSpottedState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.CHECKIFSPOTTED;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered CheckIfSpotted state");
        if(ownerTank.closestEnemy == null)
        {
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting CheckIfSpotted state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating CheckIfSpotted state");
        if (ownerTank.closestEnemy == null)
        {
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }

        //If enemy has spotted you
        if (IsEnemyLooking())
        {
            //Check if in range
            stateMachine.ChangeState(StateID.CHECKIFINRANGE);
        }
        //If not spotted by Enemy
        else
        {
            //If not spotted and can't see enemy
            if(ownerTank.seenEnemies.Count == 0)
            {
                //Find random pos and then Go back to wander
                stateMachine.ChangeState(StateID.FINDRANDOMPOS);

            }
            //If i can see enemy
            else
            {
                //Check if in range
                stateMachine.ChangeState(StateID.CHECKIFINRANGE);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool IsEnemyLooking()
    {
        //For each seen enemy in Enemy's list
        foreach(Transform t in ownerTank.closestEnemy.GetComponent<Tank>().seenEnemies)
        {
            //If instance in SeenEnemies list is me
            if(t == ownerTank.transform)
            {
                return true;
            }
        }
        return false;
    }
}
