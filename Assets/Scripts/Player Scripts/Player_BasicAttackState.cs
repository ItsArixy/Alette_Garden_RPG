using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private const int FirstAttackIndex = 1; //starting int for attack combo. cannot be changed by default.
    private float attackVelocityTimer; //timer for animation seuqneces changes through deltatime. changes in the player script.
    private int comboIndex = 1; //creates seuqnce to first animation by default on run of the first script
    private int comboLimit = 3; //current limit of basic attacks performed (maybe add more with an object in game??)
    private float lastTimeAttacked = 1f; //Tracks the moment when the player starts an attack in game, used for combo attack strings.
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        AttackComboIndexControllerIfNeeded();
        ApplyAttackVelocity();
        //check conditions in the unity animation state machine. Sets the int component condition the animation to switch between animations.
        anim.SetInteger("BasicAttackIndex", comboIndex);
    }


    public override void Update()
    {
        base.Update();
        handleVelocity();
        
        if (triggerCalled)
        {
            //returns to idle state after animation is finished. 
            stateMachine.ChangeState(player.idleState);
        }
        

    }

    public override void Exit()
    {
        base.Exit();
        lastTimeAttacked = Time.time; //get the current time elapsed in game
        comboIndex += 1;
    }

    public void handleVelocity()
    {
        //Check for player velocity to prevent animations clipping + speed control
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0) {
            player.SetVelocity(0, rb.velocity.y);
        }
    }

    public void ApplyAttackVelocity()
    {
        //player moves forward slightly with attacking for weight
        attackVelocityTimer = player.AttackVelocityDuration;
        player.SetVelocity(player.AttackSpeed.x * player.facingDirection, player.AttackSpeed.y);
    }


    private void AttackComboIndexControllerIfNeeded()
    {
        //limit of attack animation strings, if hit the final animation, return to 1st animation.
        //add delta timing functions so it could reset to first animation.
        //if the time currently in game is bigger than the last time elapsed in seconds when the player started an attack string, the animations will reset back to the first animarion.
        if (Time.time > lastTimeAttacked + player.comboResetTime || comboIndex > comboLimit) {
            comboIndex = FirstAttackIndex;
        }
    }
}
