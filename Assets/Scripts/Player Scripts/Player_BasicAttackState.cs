using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private const int FirstAttackIndex = 1; //starting int for attack combo. cannot be changed by default.
    private float attackVelocityTimer; //timer for animation seuqneces changes through deltatime. changes in the player script.
    private int comboIndex = 1; //creates seuqnce to first animation by default on run of the first script
    private int comboLimit = 3; //current limit of basic attacks performed (maybe add more with an object in game??)
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        AttackComboIndexControllerIfNeeded();
        ApplyAttackVelocity();
        anim.SetInteger("BasicAttackIndex", comboIndex);
    }


    public override void Update()
    {
        base.Update();
        handleVelocity();
        
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        

    }

    public override void Exit()
    {
        base.Exit();
        comboIndex += 1;
    }

    public void handleVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0) {
            player.SetVelocity(0, rb.velocity.y);
        }
    }

    public void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.AttackVelocityDuration;
        player.SetVelocity(player.AttackSpeed.x * player.facingDirection, player.AttackSpeed.y);
    }


    private void AttackComboIndexControllerIfNeeded()
    {
        if (comboIndex > comboLimit)
        {
            comboIndex = FirstAttackIndex;
        }
    }
}
