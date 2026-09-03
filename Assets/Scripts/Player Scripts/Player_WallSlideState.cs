using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (input.PlayerActionMap.Jump.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            // flips the player back around to face the other side (may change for the alette sprite making)
            player.Flip();
        }

    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0) {
            player.SetVelocity(0, rb.velocity.y);
        }
        else
        {
            player.SetVelocity(0, rb.velocity.y * .5f); //slows the fall speed down if the player is wall sliding
        }
    }


}
