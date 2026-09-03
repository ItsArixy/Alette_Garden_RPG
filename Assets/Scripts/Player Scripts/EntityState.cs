using UnityEngine;

public abstract class EntityState
{
    //COTROLS ALL STATES//

    //EntityState is an abstract Blueprunt and is used as a template for ALL States, functions only work if they inherit Entity Class//

    // Create a State Machine Ocject for Machine Work
    protected StateMachine stateMachine;
    protected string animBoolName;

    //Player object to change states based on players current state
    protected Player player;

    protected Animator anim; //can call on the Player variable and dynamically set anim variable.
    protected Rigidbody2D rb;
    protected PlayerInputSet input;
    protected float stateTimer; //timer for all states to inherit and mody through the class.
    protected bool triggerCalled;

    //Constructor for Class Instance
    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
    this.stateMachine = stateMachine;
    this.animBoolName = animBoolName;
        anim = player.anim;
        rb = player.rb;
        input = player.input;
    
    }

    public virtual void Enter()
    {
        //every time a state is changed, Enter will be called
        anim.SetBool(animBoolName, true);
        triggerCalled = false;

    }

    public virtual void Update()
    {
        //logic of the state
        anim.SetFloat("yVelocity", rb.velocityY);

        //dash setting
        if (input.PlayerActionMap.Dash.WasPressedThisFrame() && canDash())
        {
            stateMachine.ChangeState(player.dashState);
        }

        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit() {
        // every time a state ends, exit will be called to change to a new state
        anim.SetBool(animBoolName, false);
    }

    private bool canDash()
    {
        if (player.wallDetected)
        {
            return false;
        }

        if(stateMachine.currentState == player.dashState)
        {
            return false;
        }
        return true;
    }

    public void callAnimationTrigger() {
        triggerCalled = true;
    }
}
