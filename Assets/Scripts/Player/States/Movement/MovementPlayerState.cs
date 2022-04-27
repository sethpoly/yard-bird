using System.Collections;
using System.Collections.Generic;
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

        // TODO: Add movement logic
        _player.playerMovement.UpdateMovement();
        _player.playerMovement.UpdateMouseLook();

        // TODO: Add exit logic to change state
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
