using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates 
{
    public enum PlayerFSMStateType
    {
        IDLE = 0,
        MOVEMENT = 1,
        DEAD = 2
    }
}

public class Player : MonoBehaviour
{
    public PlayerFSM playerFSM = null;

    public PlayerMovement playerMovement;

    void Start()
    {
        playerFSM = new PlayerFSM();

        // Add FSM states
        playerFSM.Add(new IdlePlayerState(this));
        playerFSM.Add(new MovementPlayerState(this));

        // Set starting state
        playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        playerFSM.Update();  
    }

    void FixedUpdate()
    {
        playerFSM.FixedUpdate();
    }
}
