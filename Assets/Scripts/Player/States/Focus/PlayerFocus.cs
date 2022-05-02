using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{

    [SerializeField] private KeyCode focusKey;
    [SerializeField] private Transform hand;
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private float maxFocusDistance = .55f;

    private bool dragging = false;
    private bool resetting = false;
    private Vector3 startHandPosition;

    private void Start() {
        startHandPosition = hand.localPosition;
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
            Vector3 dir = Vector3.forward * (dragSpeed * axis) * Time.deltaTime;

            // Correct localPosition
            ClampFocusDistance();
            
            // Apply translation only during mouse movement
            if(HasMouseYMoved()) 
            {
                if(hand.localPosition.z >= startHandPosition.z && hand.localPosition.z < startHandPosition.z + maxFocusDistance) 
                    hand.Translate(dir);
            }
        }
        else 
        {
            // Start resetting focus
            ResetAction();
        }
    }

    // Clamp focus reach between bounds
    private void ClampFocusDistance()
    {
        if(hand.localPosition.z > startHandPosition.z + maxFocusDistance)
            hand.localPosition = Vector3.MoveTowards(hand.localPosition, startHandPosition, dragSpeed * Time.deltaTime);
        else if(hand.localPosition.z < startHandPosition.z)
            hand.localPosition = Vector3.MoveTowards(hand.localPosition, startHandPosition, dragSpeed * Time.deltaTime);
    }

    private void ResetAction()
    {
        resetting = true;
        hand.localPosition = Vector3.MoveTowards(hand.localPosition, startHandPosition, dragSpeed * Time.deltaTime);

        if(hand.localPosition == startHandPosition) {
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
