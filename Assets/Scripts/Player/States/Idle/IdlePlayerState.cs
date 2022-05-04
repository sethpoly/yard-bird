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

        // Check exit inputs
        movementInput = _player.playerMovement.AnyInput();

        if(movementInput)
            _player.playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.MOVEMENT);


        // Add state logic
        _player.playerMovement.UpdateMouseLook();
        _player.hand.UpdateEquipmentLogic();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
