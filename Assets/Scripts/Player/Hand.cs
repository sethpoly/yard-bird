using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    private Player player = null;
    public Equipment equipped = null;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void UpdateEquipmentLogic() 
    {
        if(equipped != null)
        {
            equipped.CheckMainUse();
            equipped.CheckAltUse();
        }
    }

    public void Equip(Equipment equipment)
    {
        equipped = equipment;
        equipment.transform.SetParent(this.transform);
    }

    /// <summary>
    /// Check if the hand is holding something
    /// </summary>
    public bool IsArmed()
    {
        return equipped != null;
    }

    public void CheckInteractableObjects()
    {
        RaycastHit? hit = ScanInteractableRadius();

        if(hit.HasValue)
        {
            if(Input.GetKeyDown(interactKey))
                ProcessInteraction(hit.Value);

            IInteractable interactable = hit.Value.collider.gameObject.GetComponent<IInteractable>();
            if(interactable != null) 
            {
                canvasController.ShowInteractPromptText(interactKey, interactable.GetInteractionPrompt());
            }
        } else {
            canvasController.HideInteractPromptText();
        }
    }

    private RaycastHit? ScanInteractableRadius()
    {
        float thickness =  player.playerMovement.controller.height / 5;
        float maxDistance = 2f;
        RaycastHit hit;
        Vector3 origin = player.transform.position + player.playerMovement.controller.center;
        Vector3 direction = player.transform.forward;
        if (Physics.SphereCast(origin, thickness, direction, out hit, maxDistance, interactableLayer)) 
        {
            Debug.Log("Detected IInteractable: " + hit.collider.name);
            return hit;
        }
        return null;
    }

    private void ProcessInteraction(RaycastHit hit)
    {
        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
        if(interactable != null) 
        {
            interactable.Interaction(this.gameObject);
        }
    }
}
