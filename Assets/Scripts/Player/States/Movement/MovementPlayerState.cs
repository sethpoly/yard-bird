using UnityEngine;

public class MovementPlayerState : PlayerFSMState
{
    public MovementPlayerState(Player player) : base(player)
    {
        _id = PlayerStates.PlayerFSMStateType.MOVEMENT;
    }

    public override void Enter()
    {
        base.Enter();
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
