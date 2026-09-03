using UnityEngine;

public class PlayerFallState : Player_AiredState
{
    public PlayerFallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();
        //check if the player is hitting the ground, if yes, switch to idle state

        //if player taps the jump button again, switches to float state while off the ground
        if (input.PlayerActionMap.Jump.WasPerformedThisFrame())
        {
            Debug.Log("Floating"); //for thr floating mechanic
            stateMachine.ChangeState(player.floatState);
        }

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState); //checks the collison if the player is hitting the ground layer. If yes, return to idle position
        }

        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}



