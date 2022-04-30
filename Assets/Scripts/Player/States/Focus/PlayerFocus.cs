using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] private KeyCode focusKey;

    [SerializeField] private Transform hand;
    private float handPitch = 0.0f;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update focus action of moving "hand" forward and back in a thrusting motion
    public void UpdateAction()
    {
        Vector2 targetMouseDelta = new Vector2(0, Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, 0f);

        handPitch -= currentMouseDelta.y * 5f;
        handPitch = Mathf.Clamp(handPitch, -90.0f, 90.0f);

        hand.localEulerAngles = Vector3.right * handPitch;

        hand.Rotate(Vector3.up * currentMouseDelta.x * 5f);
    }

    // Check if relevant input should set this state as current
    public bool AnyInput()
    {
        return Input.GetKey(focusKey);
    }
}
