using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class WallJump_State : EntityState
{
    public WallJump_State(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }
    
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(player.wallJumpForce.x * -player.facingDirection, player.wallJumpForce.y);
    }

    public override void Update()
    {
            base.Update();
        if (rb.velocity.y < 0) {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.wallDetected) {
            stateMachine.ChangeState(player.wallSlideState);
        }

    }
}
