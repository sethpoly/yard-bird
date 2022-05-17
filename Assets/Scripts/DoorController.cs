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
    private string currentPrompt = null;
    [SerializeField] private string openPrompt = "Open door";
    [SerializeField] private string closePrompt = "Close door";

    private void Start() {
        currentPrompt = openPrompt;
    }

    void Update()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    public bool Interaction(GameObject interacter)
    {
        if(!animator.IsInTransition(0)) 
        {
            switch(doorState)
            {
                case DoorState.OPEN:
                    animator.Play("DoorClose");
                    doorState = DoorState.CLOSED;
                    currentPrompt = openPrompt;
                    break;
                case DoorState.CLOSED:
                    animator.Play("DoorOpen");
                    doorState = DoorState.OPEN;
                    currentPrompt = closePrompt;
                    break;
            }
            return true;
        }
        return false;
    }

    public string GetInteractionPrompt()
    {
        return currentPrompt;
    }
}
