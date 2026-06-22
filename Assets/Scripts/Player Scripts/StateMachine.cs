using UnityEngine;

public class StateMachine
{
    //Controls the EntityState, defines the states for Entities
    //refrence to the current state
    public EntityState currentState{get; private set;} //the current state the object is in

    public void Initalize(EntityState startState) //the state at the game's start
    {

    currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState) //change the state
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }
 
}
