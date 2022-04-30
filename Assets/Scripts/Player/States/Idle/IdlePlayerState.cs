using System.Collections;
using UnityEngine;

public class IdlePlayerState : PlayerFSMState
{

    private bool movementInput = false;
    private bool focusInput = false;

    public IdlePlayerState(Player player): base(player) 
    {
        _id = PlayerStates.PlayerFSMStateType.IDLE;
    }

     public override void Enter()
    {
        base.Enter();
        movementInput = false;
        focusInput = false;
    }

    public override void Update()
    {
        base.Update();

        // Add state logic
        _player.playerMovement.UpdateMouseLook();

        // Check exit inputs
        movementInput = _player.playerMovement.AnyInput();
        focusInput = _player.playerFocus.AnyInput();

        // Perform exits from inputs
        if(movementInput)
            _player.playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.MOVEMENT);

        if(focusInput)
            _player.playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.FOCUS);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
