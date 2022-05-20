using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject equipmentText;
    [SerializeField] private GameObject interactText;
    private PromptText equipmentPrompt;
    private PromptText interactPrompt;


    // Start is called before the first frame update
    void Start()
    {
        equipmentPrompt = equipmentText.GetComponent<PromptText>();
        interactPrompt = interactText.GetComponent<PromptText>();
    }

    public void ShowEquipmentPromptText(KeyCode keyCode, string text)
    {
        equipmentPrompt.SetText(keyCode, text);
        equipmentPrompt.On();
    }

    public void HideEquipmentPromptText()
    {
        equipmentPrompt.Off();
    }

    public void ShowInteractPromptText(KeyCode keyCode, string text)
    {
        interactPrompt.SetText(keyCode, text);
        interactPrompt.On();
    }

    public void HideInteractPromptText()
    {
        interactPrompt.Off();
    }
}
