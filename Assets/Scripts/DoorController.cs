using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    enum DoorState {
        OPEN,
        CLOSED,
    }

    [SerializeField] private Animator animator = null;
    private DoorState doorState = DoorState.CLOSED;

    public bool Interaction(GameObject interacter)
    {
        switch(doorState)
        {
            case DoorState.OPEN:
                animator.Play("DoorClose");
                doorState = DoorState.CLOSED;
                break;
            case DoorState.CLOSED:
                animator.Play("DoorOpen");
                doorState = DoorState.OPEN;
                break;
        }
        return true;
    }

    public string GetInteractionPrompt()
    {
        return "Open door";
    }
}
