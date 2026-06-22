using JetBrains.Annotations;
using UnityEngine;

public class Player_DashState : EntityState
{
    private float originalGravityScale;
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter(); //when enetering the state, set the inital timer for the state, inherited in entitystate
        stateTimer = player.dashDuration;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {

        base.Update();
        player.SetVelocity(player.dashSpeed * player.facingDirection, 0);
        CheckDashCancel();
        if (stateTimer <= 0 && player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }
    
    public void CheckDashCancel()
    {
        if (stateTimer <= 0)
        {
            if (player.wallDetected && !player.groundDetected)
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }

    }
  

}

// Start is called once before the first execution of Update after the MonoBehaviour is created

