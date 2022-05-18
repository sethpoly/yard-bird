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
    [SerializeField] private string openPrompt = "Open door";
    [SerializeField] private string closePrompt = "Close door";
    private DoorState doorState = DoorState.CLOSED;
    private string currentPrompt = null;
    private BoxCollider boxCollider;

    private void Start() {
        currentPrompt = openPrompt;
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(IsAnimating())
            DisableColliders();
         else 
            EnableColliders();
    }

    public bool Interaction(GameObject interacter)
    {
        if(!IsAnimating()) 
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

    private bool IsAnimating()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    private void EnableColliders()
    {
        boxCollider.enabled = true;
    }

    private void DisableColliders()
    {
        boxCollider.enabled = false;
    }
}
