using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    //---Input Systems---
    public PlayerInputSet input { get; private set; } //Input System variable
    private StateMachine stateMachine;

    //---- Player States-----
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public WallJump_State wallJumpState { get; private set; }
    public Player_FloatState floatState { get; private set; }

    public Player_DashState dashState { get; private set; }

    public Player_BasicAttackState basicAttackState { get; private set; }
    //---- Player States-----
    public Vector2 moveInput { get; private set; }


    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;

    public Vector2 wallJumpForce;
    private bool facingRight = true;
    public int facingDirection { get; private set; } = 1;
    [Range(0, 1)]
    public float airMultiplier = 0.7f; //0-1 only //Handles air velocity (goes between 0-1 ranges for velovcity modifications).
    [Space]
    public float dashSpeed = 20;
    public float dashDuration = 0.25f;

    [Header("Attack Details")]
    public Vector2 AttackSpeed;
    public float AttackVelocityDuration = .1f;
    public float comboResetTime = 1f;


    [Header("Collision Detetcion")]
    [SerializeField] private float groundCheckDistance = 1.45f;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    [Header("Player Stats")]
    [SerializeField] private int AttackStat = 10;
    [SerializeField] private int DefStat = 10;

    private void Awake()
    {
       anim = GetComponentInChildren<Animator>(); // always asign the animator first
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine(); //state machine to transfer states
        input = new PlayerInputSet();

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); //state names now are boolean checks to see if animations play
        moveState = new PlayerMoveState(this, stateMachine, "move"); //names must match in Unity Game Editor
        jumpState = new PlayerJumpState(this, stateMachine, "JumpFall"); //Controls the jump and falling animations and logic
        fallState = new PlayerFallState(this, stateMachine, "JumpFall"); // controlls the falling animations and logic
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide"); //state machine to conteol sliding on walls
        wallJumpState = new WallJump_State(this, stateMachine, "JumpFall");
        floatState = new Player_FloatState(this, stateMachine, "float");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.callAnimationTrigger();

    }

    private void OnEnable()
    {
        input.Enable(); // enables the input system for player actions
        //input.PlayerScript.Movement.started; - input has began
        //input.PlayerScript.Movement.perfromed; - input is performed
        //input.PlayerScript.Movement.canceled; - input stops
        input.PlayerActionMap.Movement.performed += context => moveInput = context.ReadValue<Vector2>(); // dynamic movement
        input.PlayerActionMap.Movement.canceled += context => moveInput = Vector2.zero; //reduces changes to 0
    }

    private void OnDisable()
    {
        input.Disable(); //kill or destroy object for example
    }

    private void Start()
    {
        stateMachine.Initalize(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();
        HandleCollisonDetection();
    }

    public void SetVelocity(float xVelcoity, float yVelcoity)
    {
        rb.velocity = new Vector2(xVelcoity, yVelcoity);
        FlipController(xVelcoity);
    }

    private void FlipController(float xVelocity)
    {
        if(xVelocity > 0 && facingRight == false) 
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight)
        {
            Flip();
        }

    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDirection *= -1;
    }

    private void HandleCollisonDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        //debug tool to find collisions
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance *facingDirection, 0));
    }
}
