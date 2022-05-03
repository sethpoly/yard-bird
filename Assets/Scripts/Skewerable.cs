using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Interaction(GameObject interacter) 
    {
        Debug.Log("Trying to interact with a Skewerable object!");

        // Reassign position
        rb.detectCollisions = false;
        rb.isKinematic = true;

        if(stuckTo == null || stuckTo != interacter.transform)
        {
            stuckTo = interacter.transform;
        }
    }
}



