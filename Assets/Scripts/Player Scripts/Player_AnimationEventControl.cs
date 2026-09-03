using UnityEngine;

public class Player_AnimationEventControl : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>(); //inherits objects and methods from the parent script
        
    }

    public void currentStateTrigger()
    {
        player.CallAnimationTrigger(); //toggles animations and states for the attack sequence (update for combo sequence later)
    }

}
