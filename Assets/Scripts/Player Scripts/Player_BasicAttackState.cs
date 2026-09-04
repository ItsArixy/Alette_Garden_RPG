using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private const int FirstAttackIndex = 1; //starting int for attack combo. cannot be changed by default.
    private float attackVelocityTimer; //timer for animation seuqneces changes through deltatime. changes in the player script.
    private int comboIndex = 1; //creates seuqnce to first animation by default on run of the first script
    private int comboLimit = 3; //current limit of basic attacks performed (maybe add more with an object in game??)
    private float lastTimeAttacked = 1f; //Tracks the moment when the player starts an attack in game, used for combo attack strings.
    private bool attackQueued; //seamless animation tracking
    private int attackDir; //used to havw the player change direction based on where they want to attack through player input.
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        //animation boundary check
        //if(comboLimit != player.AttackSpeed.Length)
        //{
            //comboLimit = player.AttackSpeed.Length;

        //}

    }

    public override void Enter()
    {
        base.Enter();
        attackQueued = false;
        AttackComboIndexControllerIfNeeded();
        //check conditions in the unity animation state machine. Sets the int component condition the animation to switch between animations.
        anim.SetInteger("BasicAttackIndex", comboIndex);
        //change condition if the player doesn't attack within the next sample set of seconds

        //changes to direction if player moves while attacking
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDirection;
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        handleVelocity();
        

        if (input.PlayerActionMap.BasicAttack.WasPressedThisFrame())
        {
            Debug.Log("key was pressed");
            queueNextAttack();
        }

        if (triggerCalled)
        {
            //returns to idle state IF the player. 
            HandleExit();

        }
    }

    private void HandleExit()
    {
        if (attackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterBasicAttackStateWithDelay(); //check enter animation state or entitystate as to why combos are automatically going off
        }
        else
        {
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
        //apply variation of attack velocity into the player animation script tied to the index of the animation playing. 
        Vector2 attackVelocity = player.AttackSpeed[comboIndex - 1];
        //player moves forward slightly with attacking for weight
        attackVelocityTimer = player.AttackVelocityDuration;
        //dynamically sets the velocity on the vector for the player to move while attacking with the current facing direction.
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
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

    public void queueNextAttack()
    {
        if (comboIndex < comboLimit) {
            attackQueued = true;
        }
    }
}
