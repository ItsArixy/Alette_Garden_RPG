using UnityEngine;

public class PlayerIdleState : Player_GroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string StateName) : base(player, stateMachine, StateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.velocity.y);
    }
    public override void Update()
    {
        base.Update();
        if (player.moveInput.x != 0) {
            stateMachine.ChangeState(player.moveState);
        }

    }

}
