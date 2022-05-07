using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any GameObject that is able to be skewered should equip this script, 
/// allows the GameObject to change it's origin to the interacter's origin,
/// passes a reference to this GameObject to an array the interacter holds
/// </summary>
public class Skewerable : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    private Transform stuckTo = null;
    private Quaternion randomRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randomRotation = Random.rotation;
    }

    public void LateUpdate()
    {
        if (stuckTo != null)
        {
            transform.position = stuckTo.position;
            transform.rotation = stuckTo.rotation * randomRotation;
        }
    }

    public bool Interaction(GameObject interacter) 
    {
        Debug.Log("Trying to interact with a Skewerable object!");
        return SetSkewered(interacter);
    }

    private bool SetSkewered(GameObject interacter)
    {
        // Reassign position
        rb.detectCollisions = false;
        rb.isKinematic = true;

        if(stuckTo == null || stuckTo != interacter.transform)
        {
            stuckTo = interacter.transform;
            return true;
        }
        return false;
    }

    public void SetIdle()
    {
        // Reassign position
        rb.detectCollisions = true;
        rb.isKinematic = false;
        stuckTo = null;
    }
}



