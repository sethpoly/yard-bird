using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCage : MonoBehaviour
{
    [SerializeField] private float fallForce = 2f;
    private Animator animator = null;
    private Rigidbody rb;


    enum CageEvent
    {
       fall,
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get animator in child
        animator = GetComponentInChildren<Animator>();
    }

    void StartCageEvent(CageEvent cageEvent)
    {
        switch(cageEvent) 
        {
            case CageEvent.fall: 
                StartFallEvent();
                break;
        }
    }
    

    // Event that occurs during the shack event, the cage flies off the shelf and it's door opens
    private void StartFallEvent() {
        // Apply forward force to rb
        FallOffShelf();

        // TODO: Play cage falling sound

        // Open cage door
        OpenCageDoor();

        // Remove bird
        KillBird();
    }

    // Apply enough force to fly off shelf
    private void FallOffShelf()
    {
        rb.AddForce((transform.forward + new Vector3(Random.Range(0, 0.4f), 0, 0)) * fallForce);
    }

    private void OpenCageDoor() {
        animator.Play("CageDoorOpen");
    }

    private void KillBird()
    {
        if(GetComponentInChildren<lb_Bird>())
        {
            GetComponentInChildren<lb_Bird>().Suicide();
        }
    }    
}
