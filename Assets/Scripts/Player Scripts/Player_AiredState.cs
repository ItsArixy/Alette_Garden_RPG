using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // Update is called once per frame
   public override  void Update()
    {
        base.Update();
        if(player.moveInput.x != 0)
        {
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.airMultiplier), rb.velocity.y);
        }
        
    }
}
