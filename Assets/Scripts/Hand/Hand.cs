using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandStates 
{
    public enum HandFSMStateType
    {
        EMPTY = 0,
    }
}

public class Hand : MonoBehaviour
{
    public HandFSM handFSM = null;

    void Start()
    {
        handFSM = new HandFSM();

        // Add states
        handFSM.Add(new EmptyHandState(this));

        // Set current
        handFSM.SetCurrentState(HandStates.HandFSMStateType.EMPTY);
    }

    void Update()
    {
        handFSM.Update();
    }

    void FixedUpdate() 
    {
        handFSM.FixedUpdate();   
    }
}
