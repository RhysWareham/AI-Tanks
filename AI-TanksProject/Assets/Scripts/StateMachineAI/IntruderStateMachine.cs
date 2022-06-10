using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntruderStateMachine : StateMachine
{
    public IdleState idleState { get; private set; }
    public WanderState wanderState { get; private set; }
    public FindRandomPositionState findRandomPosState { get; private set; }
    public FindClosestEnemyState findClosestEnemyState { get; private set; }
    public CheckIfSpottedState checkIfSpottedState { get; private set; }
    public CheckIfInRangeState checkIfInRangeState { get; private set; }
    public ShootState shootState { get; private set; }
    public RunawayState runawayState { get; private set; }
    public FindCloserPosState findCloserPosState { get; private set; }
    public DeathState deathState { get; private set; }


    public IntruderStateMachine(Tank owner) : base(owner)
    {
        idleState = new IdleState(ownerTank, this);
        wanderState = new WanderState(ownerTank, this);
        findRandomPosState = new FindRandomPositionState(ownerTank, this);
        findClosestEnemyState = new FindClosestEnemyState(ownerTank, this);
        checkIfSpottedState = new CheckIfSpottedState(ownerTank, this);
        checkIfInRangeState = new CheckIfInRangeState(ownerTank, this);
        shootState = new ShootState(ownerTank, this);
        runawayState = new RunawayState(ownerTank, this);
        findCloserPosState = new FindCloserPosState(ownerTank, this);
        deathState = new DeathState(ownerTank, this);

        //RegisterState(idleState);
        //RegisterState(wanderState);
        //RegisterState(findRandomPosState);
        //RegisterState(findClosestEnemyState);
        //RegisterState(checkIfSpottedState);
        //RegisterState()

    }
}
