using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayerState : PlayerFSMState
{
    public FocusPlayerState(Player player): base(player)
    {
        _id = PlayerStates.PlayerFSMStateType.FOCUS;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // Add movement logic

        // Check exit inputs

        // Perform exit patterns for inputs
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
