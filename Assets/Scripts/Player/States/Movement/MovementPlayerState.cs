using UnityEngine;

public class MovementPlayerState : PlayerFSMState
{
    private bool idleInput = false;

    public MovementPlayerState(Player player) : base(player)
    {
        _id = PlayerStates.PlayerFSMStateType.MOVEMENT;
    }

    public override void Enter()
    {
        base.Enter();
        idleInput = false;
    }

    public override void Update()
    {
        base.Update();

        // Add logic
        _player.playerMovement.UpdateMovement();
        _player.playerMovement.UpdateMouseLook();
        _player.hand.UpdatePoke();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
