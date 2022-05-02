using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] private KeyCode focusKey;
    [SerializeField] private Transform hand;
    [SerializeField] private float dragSpeed = 1f;

    private bool dragging = false;
    private bool resetting = false;
    private Vector3 startHandPosition;

    // Set the start hand position to reset the focus on state enter
    public void SetStartHandPosition()
    {
        startHandPosition = hand.position;
    }

    // Update focus action of moving "hand" forward and back in a thrusting motion
    public void UpdateAction()
    {
        // Axis direction to move hand transform
        float axis = 0;

        // Determine if we are dragging
        dragging = Input.GetKey(focusKey);
        
        if(dragging) 
        {
            if (Input.GetKeyUp(focusKey))
                dragging = false;
    
            // Retrieve if mouse is moving up or down
            axis = Input.GetAxis("Mouse Y") > 0 ? 1f : -1f; 

            // Apply translation only during mouse movement
            if(HasMouseYMoved()) 
                hand.Translate(Vector3.forward * (dragSpeed * axis) * Time.deltaTime);
        }
        else 
        {
            // Start resetting focus
            ResetAction();
        }
    }

    private void ResetAction()
    {
        Debug.Log("Resetting");
        resetting = true;
        hand.position = Vector3.MoveTowards(hand.position, startHandPosition, dragSpeed * Time.deltaTime);

        if(hand.position == startHandPosition) {
            resetting = false;
        }
    }


    // Determine if mouseY has moved this frame    
    private bool HasMouseYMoved()
    {
        return (Input.GetAxis("Mouse Y") != 0);
    }

    
    // Check if relevant input should set this state as current
    public bool AnyInput()
    {
        return Input.GetKey(focusKey) || resetting;
    }
}
