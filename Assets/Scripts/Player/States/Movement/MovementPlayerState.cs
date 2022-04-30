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
        _player.playerMovement.UpdateMovement();
    }

    public override void Update()
    {
        base.Update();

        // Add movement logic
        _player.playerMovement.UpdateMovement();
        _player.playerMovement.UpdateMouseLook();

        // Check exit inputs
        idleInput = _player.playerMovement.controller.velocity == Vector3.zero && _player.playerMovement.controller.isGrounded;

        // Perform exit patterns for inputs
        if(idleInput)
            _player.playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.IDLE);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
