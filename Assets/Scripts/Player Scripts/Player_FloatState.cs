using UnityEngine;

public class Player_FloatState : Player_AiredState
{
    public Player_FloatState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // State Logic: Can only be used while in the air, slows the player's gravity down. Pressing space once activates it, pressing space again while float is active returns gravity to normal.
    // removes the state


    public override void Enter()
    {
        base.Enter();
    }

    public override void Update() {
        base.Update();
        floatHandle();


        if (input.PlayerActionMap.Jump.WasPerformedThisFrame())
        {
            player.SetVelocity(0, rb.velocity.y);
            stateMachine.ChangeState(player.fallState); //when the jump button is pressed again, return to the fall state
            //change float velocity back to normal.  
        }

        if (player.groundDetected) {
            //if the player touches the ground, revert back to idle state
            player.SetVelocity(0, rb.velocity.y);
            stateMachine.ChangeState(player.idleState);
        }

        if (player.wallDetected)
        {
            player.SetVelocity(0, rb.velocity.y);
            stateMachine.ChangeState(player.wallSlideState);
        }

    }

    private void floatHandle()
    {
        //change the velocity of the float state when entering.
        if (player.moveInput.y < 0)
        {
            if(player.moveInput.x == 0) //if the player is pressing left or right dont glide forward
            {
                player.SetVelocity(0, rb.velocity.y);
            }
            else
            {
                player.SetVelocity(player.moveInput.x * player.facingDirection, rb.velocity.y);
            }
                
        }
        else
        {
            if (player.moveInput.x == 0) //if the player is pressing left or right dont glide forward
            {
                player.SetVelocity(0, rb.velocity.y * .5f);
            }
            else
            {
                player.SetVelocity(player.moveInput.x * player.facingDirection, rb.velocity.y * .5f);
            }
            player.SetVelocity(player.moveInput.x * player.facingDirection, rb.velocity.y * .5f); //slows the fall speed down if the player is floating
        }


    }
}
