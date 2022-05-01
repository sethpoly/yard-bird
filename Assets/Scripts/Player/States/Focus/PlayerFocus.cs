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

    void Start() {
        startHandPosition = hand.position;
    }

    // Update focus action of moving "hand" forward and back in a thrusting motion
    public void UpdateAction()
    {
        // Axis direction to move hand transform
        float axis = 0;

        // Determine if we are dragging
        dragging = Input.GetKey(focusKey);
        //ResetAction();

        // If not holding focus button, return
        if (!dragging || !this.enabled)
            return;
        
        //startHandPosition = hand.position;

        // If focus released, reset flag this frame
        if (Input.GetKeyUp(focusKey))
            dragging = false;
 
        // Retrieve if mouse is moving up or down
        axis = Input.GetAxis("Mouse Y") > 0 ? 1f : -1f; 

        // Apply translation only during mouse movement
        if(HasMouseYMoved()) 
            hand.Translate(Vector3.forward * (dragSpeed * axis) * Time.deltaTime);
    }

    private void ResetAction()
    {
        if(!dragging)
        {
            Debug.Log("Resetting");
            resetting = true;
            hand.position = Vector3.MoveTowards(hand.position, startHandPosition, dragSpeed * Time.deltaTime);

        }
        else 
            resetting = false;
    }


    // Determine if mouseY has moved this frame    
    private bool HasMouseYMoved()
    {
        //I feel dirty even doing this 
        return (Input.GetAxis("Mouse Y") != 0);
    }

    
    // Check if relevant input should set this state as current
    public bool AnyInput()
    {
        return Input.GetKey(focusKey) || resetting;
    }
}
