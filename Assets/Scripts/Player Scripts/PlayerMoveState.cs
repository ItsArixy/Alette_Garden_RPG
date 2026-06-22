using UnityEngine;

public class PlayerMoveState : Player_GroundedState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, string StateName) : base(player, stateMachine, StateName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.velocityY);
    }
}
