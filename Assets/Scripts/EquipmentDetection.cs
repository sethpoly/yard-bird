using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects if nearby an equippable equipment, allows player to interact via 'UseKey'
/// </summary>
public class EquipmentDetection : MonoBehaviour
{
    [SerializeField] private CanvasController canvasController;
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
        // Only monitor radius if hand is unarmed
        if(!hand.IsArmed())
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

            // Show prompt text
            if(!IsAlreadyEquipped(hit.Value.collider.gameObject)) 
                ShowPromptText();
        } else 
        {
            HidePromptText();
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
            Debug.Log("Processed equipment found -> " + hit.collider.name);
            equipment.AttachEquipment(player, hand);
            HidePromptText();
        }   
    }

    private bool IsAlreadyEquipped(GameObject equipment)
    {
        return equipment.GetComponent<Equipment>().equipped;
    }

    private void ShowPromptText()
    {
        canvasController.ShowPromptText("Press " + ((char)equipKey) + " to pick up");
    }

    private void HidePromptText()
    {
        canvasController.HidePromptText();
    }
}
