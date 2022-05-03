using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    enum PokeState
    {
        NONE,
        POKE,
        RESET
    }

    [SerializeField] private KeyCode pokeKey;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform skewerTip;
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private float maxFocusDistance = .8f;
    [SerializeField] private int focusHitDistanceInMeters = 2;
    [SerializeField] private LayerMask focusHitLayer;

    private PokeState pokeState = PokeState.NONE;
    private Vector3 startHandPosition;

    private void Start() {
        startHandPosition = hand.localPosition;
    }

    public void UpdatePoke()
    {

        Vector3 targetHandPosition = new Vector3(startHandPosition.x, startHandPosition.y, startHandPosition.z + maxFocusDistance);

        if(Input.GetKeyDown(pokeKey) && pokeState != PokeState.POKE)
            pokeState = PokeState.POKE;

        switch(pokeState)
        {
            case PokeState.NONE:
                break;
            case PokeState.POKE:
                UpdatePokeState(targetHandPosition);
                break;
            case PokeState.RESET:
                UpdateResetState();
                break;
        }
    }

    private void UpdatePokeState(Vector3 targetPosition)
    {
        hand.localPosition = Vector3.MoveTowards(hand.localPosition, targetPosition, dragSpeed * Time.deltaTime);

        // Exit condition
        if(hand.localPosition.z >= targetPosition.z)
        {
            pokeState = PokeState.RESET;
            FireRay();
        }
    }

    private void UpdateResetState()
    {
        hand.localPosition = Vector3.MoveTowards(hand.localPosition, startHandPosition, dragSpeed * Time.deltaTime);   

        // Exit condition
        if(hand.localPosition.z <= startHandPosition.z)
            pokeState = PokeState.NONE;
    }

    private void FireRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawRay(ray.origin, ray.direction * 10);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, focusHitDistanceInMeters, focusHitLayer))
            ProcessRaycastHit(hit);
    }

    private void ProcessRaycastHit(RaycastHit hit)
    {
        Debug.Log("We hit a " + hit.collider.gameObject.name);

        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
        if(interactable != null) 
            interactable.Interaction(skewerTip.gameObject);
    }
}
