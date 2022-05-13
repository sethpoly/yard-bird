using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment: MonoBehaviour {

    [SerializeField] public Player player;
    [SerializeField] public Transform hand;

    public abstract void Setup();
    public abstract void CheckMainUse();
    public virtual void CheckAltUse(){
        Debug.Log("Alt use not implemented");
    }

    public void AttachEquipment(Player player, Transform hand)
    {
        this.player = player;
        this.hand = hand;
    }
}

public class Poker : Equipment
{
    enum PokeState
    {
        NONE,
        POKE,
        RESET
    }
    [SerializeField] private KeyCode pokeKey;
    [SerializeField] private KeyCode altKey = KeyCode.E;
    [SerializeField] private Transform pokerTip;
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private float maxPokePhysicalDistance = .8f;
    [SerializeField] private int pokeReachInMeters = 2;
    [SerializeField] private LayerMask objectLayer;

    // List of items currently skewered by skewer
    [SerializeField] private List<GameObject> pokedItems = new List<GameObject>();

    private PokeState pokeState = PokeState.NONE;
    private Vector3 startHandPosition;

    override public void Setup()
    {
        if(hand != null)
        {
            // Orient poker
            
            // Save start orientation
            startHandPosition = hand.localPosition;
        }
    }

    private void Start() {
        if(hand != null)
        {
            startHandPosition = hand.localPosition;
        }
    }

    override public void CheckMainUse()
    {

        Vector3 targetHandPosition = new Vector3(startHandPosition.x, startHandPosition.y, startHandPosition.z + maxPokePhysicalDistance);

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
        if(Physics.Raycast(ray, out hit, pokeReachInMeters, objectLayer))
            ProcessRaycastHit(hit);
    }

    private void ProcessRaycastHit(RaycastHit hit)
    {
        Debug.Log("We hit a " + hit.collider.gameObject.name);

        Skewerable skewerable = hit.collider.gameObject.GetComponent<Skewerable>();
        if(skewerable != null) 
        {
            bool isSuccessful = skewerable.Interaction(pokerTip.gameObject);

            // Append item to list of interact succeeded
            if(isSuccessful)
                AppendPokedItem(hit.collider.gameObject);

        }
    }

    override public void CheckAltUse() 
    {
        RaycastHit? hit = FireSphereCast();

        if(hit.HasValue)
        {
            // Check Input
            if(Input.GetKeyDown(altKey))
                ProcessSphereCast(hit.Value);
        }
    }

    private RaycastHit? FireSphereCast()
    {
        float thickness =  player.playerMovement.controller.height / 5;
        float maxDistance = 2f;
        RaycastHit hit;
        Vector3 origin = player.transform.position + player.playerMovement.controller.center;
        Vector3 direction = player.transform.forward;
        if (Physics.SphereCast(origin, thickness, direction, out hit, maxDistance, objectLayer)) 
        {
            //Debug.Log("We hit a " + hit.collider.gameObject.name);
            return hit;
        }
        return null;
    }

    private void ProcessSphereCast(RaycastHit hit)
    {
        Containerable container = hit.collider.gameObject.GetComponent<Containerable>();
            if(container != null) 
            {
                // Get next can saved
                if(pokedItems.Count <= 0) { return; }

                // Pass can to object we interacted with
                GameObject nextCan = pokedItems[0];
                if(container.Interaction(nextCan))
                    RemovePokedItem(nextCan);
            }   
    }

    private void AppendPokedItem(GameObject item)
    {
        pokedItems.Add(item);
    }

    private void RemovePokedItem(GameObject item)
    {
        pokedItems.Remove(item);
    }
}
