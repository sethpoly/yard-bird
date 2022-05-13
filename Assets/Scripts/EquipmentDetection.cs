using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects if nearby an equippable equipment, allows player to interact via 'UseKey'
/// </summary>
public class EquipmentDetection : MonoBehaviour
{
    [SerializeField] private Player player;
    private Hand hand;
    [SerializeField] private KeyCode equipKey = KeyCode.E;
    [SerializeField] private LayerMask objectLayer;

    private void Start()
    {
        if(this.GetComponent<Hand>() != null)
        {
            hand = this.GetComponent<Hand>();
        }
    }

    private void Update() 
    {
        MonitorRadius();
    }

    private void MonitorRadius()
    {
        RaycastHit? hit = FireSphereCast();

        if(hit.HasValue)
        {
            // Check Input
            if(Input.GetKeyDown(equipKey))
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
            Debug.Log("Detected Equipment: " + hit.collider.name);
            return hit;
        }
        return null;
    }

    private void ProcessSphereCast(RaycastHit hit)
    {
        Equipment equipment = hit.collider.gameObject.GetComponent<Equipment>();
        if(equipment != null) 
        {
            Debug.Log("Trying to process equipment found...");
            equipment.AttachEquipment(player, hand.transform);
            hand.Equip(equipment);
            equipment.Setup();
        }   
    }

}