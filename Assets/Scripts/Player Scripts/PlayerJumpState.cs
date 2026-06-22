using UnityEngine;

public class PlayerJumpState : Player_AiredState

{
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //change velocity of y to make the player rise
        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        //if the velocity is going down, change to fall state
        
        //if the jump button is pressed again
        if (input.PlayerActionMap.Jump.WasPerformedThisFrame())
        {
            Debug.Log("Floating"); //for thr floating mechanic
            stateMachine.ChangeState(player.floatState);
        }

        if(rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}

