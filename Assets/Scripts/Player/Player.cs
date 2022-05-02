using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates 
{
    public enum PlayerFSMStateType
    {
        IDLE = 0,
        MOVEMENT = 1,
        FOCUS = 2,
        DEAD = 3
    }
}

public class Player : MonoBehaviour
{
    public PlayerFSM playerFSM = null;

    public PlayerMovement playerMovement;
    public PlayerFocus playerFocus;
    public Hand hand;

    void Start()
    {
        playerFSM = new PlayerFSM();

        // Add FSM states
        playerFSM.Add(new MovementPlayerState(this));

        // Set starting state
        playerFSM.SetCurrentState(PlayerStates.PlayerFSMStateType.MOVEMENT);
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
