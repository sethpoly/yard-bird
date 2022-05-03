using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skewerable : MonoBehaviour, IInteractable
{

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction(GameObject interacter) 
    {
        Debug.Log("Trying to interact with a Skewerable object!");

        this.transform.parent = interacter.transform;
        rb.detectCollisions = false;
        rb.isKinematic = true;
    }
}
