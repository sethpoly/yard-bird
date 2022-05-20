using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCage : MonoBehaviour
{
    [SerializeField] private float fallForce = 2f;
    private Rigidbody rb;


    enum CageEvent
    {
       fall 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCageEvent(CageEvent.fall);
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
        // TODO: Apply forward force to rb
        FallOffShelf();

        // TODO: Play cage falling sound

        // TODO: Open cage door

        // TODO: Remove bird
    }

    private void FallOffShelf()
    {
        rb.AddForce(transform.forward * fallForce);
    }
}
