using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    protected Tank ownerTank { get; private set; }
    public StateID CurrentState { get; protected set; }
    public State[] tankStates { get; protected set; }

    public StateMachine(Tank owner)
    {
        this.ownerTank = owner;
        int numOfStates = System.Enum.GetNames(typeof(StateID)).Length;
        tankStates = new State[numOfStates];
    }

    //Puts states into stateArray
    public void RegisterState(State state)
    {
        int i = (int)state.GetID();
        tankStates[i] = state;
    }

    //Returns the state from the stateID
    public State GetState(StateID stateID)
    {
        int i = (int)stateID;
        return tankStates[i];
    }

    //Function to initialise starting state
    public void Initialise(StateID startingState)
    {
        CurrentState = startingState;
        GetState(CurrentState).Enter();
    }

    //Function to change states
    public void ChangeState(StateID newState)
    {
        GetState(CurrentState).Exit();
        CurrentState = newState;
        GetState(CurrentState).Enter();
    }

    public void Update()
    {
        GetState(CurrentState).LogicUpdate();
    }
}
