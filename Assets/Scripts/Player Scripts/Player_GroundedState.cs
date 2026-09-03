using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (input.PlayerActionMap.Jump.WasPerformedThisFrame()) { 
            stateMachine.ChangeState(player.jumpState);
        }

        if(rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (input.PlayerActionMap.BasicAttack.WasPerformedThisFrame()) {
            stateMachine.ChangeState(player.basicAttackState);
        }
    }

 
}
