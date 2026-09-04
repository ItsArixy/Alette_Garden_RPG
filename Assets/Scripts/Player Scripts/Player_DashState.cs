using JetBrains.Annotations;
using UnityEngine;

public class Player_DashState : EntityState
{
    private float originalGravityScale;
    private int dashDir;
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter(); //when enetering the state, set the inital timer for the state, inherited in entitystate
        dashDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDirection;
        stateTimer = player.dashDuration;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {

        base.Update();
        CheckDashCancel();
        player.SetVelocity(player.dashSpeed * dashDir, 0);
        if (stateTimer <= 0 && player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if(stateTimer <= 0 && !player.groundDetected)
        {
            stateMachine.ChangeState(player.fallState);
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

