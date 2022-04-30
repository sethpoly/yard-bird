using System.Collections;
using UnityEngine;

public class IdlePlayerState : PlayerFSMState
{

    private bool movementInput = false;

    public IdlePlayerState(Player player): base(player) 
    {
        _id = PlayerStates.PlayerFSMStateType.IDLE;
    }

     public override void Enter()
    {
        base.Enter();
        movementInput = false;
    }

    public override void Update()
    {
        base.Update();

        // Add state logic
        _player.playerMovement.UpdateMouseLook();

        // Check exit inputs
        movementInput = _player.playerMovement.AnyInput();

        // Perform exits from inputs
        if(movementInput)
            _player.playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.MOVEMENT);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
