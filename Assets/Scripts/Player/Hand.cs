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
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private float maxFocusDistance = .55f;

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
            pokeState = PokeState.RESET;
    }

    private void UpdateResetState()
    {
        hand.localPosition = Vector3.MoveTowards(hand.localPosition, startHandPosition, dragSpeed * Time.deltaTime);   

        // Exit condition
        if(hand.localPosition.z <= startHandPosition.z)
            pokeState = PokeState.NONE;
    }
}
