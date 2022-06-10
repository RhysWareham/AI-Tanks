using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateID
{
    IDLE,
    WANDER,
    FINDRANDOMPOS,
    FINDCLOSESTENEMY,
    CHECKIFSPOTTED,
    CHECKIFINRANGE,
    SHOOT,
    RUNAWAY,
    FINDCLOSERPOS,
    DEATH,
    NONE
}


public class State
{
    public Tank ownerTank { get; private set; }
    protected IntruderStateMachine stateMachine;


    protected float startTime;
    protected bool isExitingState;


    public State(Tank owner, IntruderStateMachine stateMachine)
    {
        this.ownerTank = owner;
        this.stateMachine = stateMachine;
        this.stateMachine.RegisterState(this);
    }

    public virtual StateID GetID()
    {
        return StateID.NONE;
    }

    //virtual means this function can be overwritten by classes that inherit from this class
    //Called when player enters a specific state
    public virtual void Enter()
    {
        DoChecks();


        startTime = Time.time;
        isExitingState = false;
    }

    //Called when player leaves a state
    public virtual void Exit()
    {

        isExitingState = true;
    }

    //Called every frame
    public virtual void LogicUpdate()
    {
        //if(ownerTank.restarting)
        //{
        //    ownerTank.restarting = false;
        //    stateMachine.ChangeState(StateID.WANDER);
            
        //}
        //If takingDmg amount is more than 0
        if(ownerTank.takingDamageAmount > 0)
        {
            ReduceMyHealth(ownerTank.takingDamageAmount);
            if(ownerTank.currentHealth <= 0)
            {
                stateMachine.ChangeState(StateID.DEATH);
            }
        }
        
        //Reduce shootTimer if more than 0
        if(ownerTank.shootTimer > 0)
        {
            ownerTank.shootTimer -= Time.deltaTime;
        }

        //If can't see enemy, rotate turret constantly
        if(ownerTank.seenEnemies.Count == 0)
        {
            ownerTank.canSeeEnemy = false;
            ownerTank.tankTurret.transform.Rotate(new Vector3(0, NeededVariables.TURRETROTATION / 2 * Time.deltaTime, 0));
        }
        else
        {
            ownerTank.canSeeEnemy = true;
        }


        
    }

    //Called every fixed update
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }


    public virtual void DoChecks()
    {

    }

    public void ReduceMyHealth(float dmgAmount)
    {
        ownerTank.currentHealth -= dmgAmount;
        ownerTank.SetHealthUI();
        ownerTank.takingDamageAmount = 0;
    }
}
