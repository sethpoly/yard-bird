using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayerState : PlayerFSMState
{
    private bool idleInput = false;

    public FocusPlayerState(Player player): base(player)
    {
        _id = PlayerStates.PlayerFSMStateType.FOCUS;
    }

    public override void Enter()
    {
        base.Enter();
        idleInput = false;
    }

    public override void Update()
    {
        base.Update();

        // Add state logic
        // TODO: _player.playerFocus.UpdateMouse();
        //_player.playerFocus.UpdateAction();

        // Check exit inputs
        //idleInput = !_player.playerFocus.AnyInput();

        // Perform exit patterns for inputs
        if(idleInput)
            _player.playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.IDLE);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
