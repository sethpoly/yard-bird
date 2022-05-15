using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject promptText;
    private PromptText po;

    // Start is called before the first frame update
    void Start()
    {
        po = promptText.GetComponent<PromptText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPromptText(KeyCode keyCode, string text)
    {
        po.SetText(keyCode, text);
        po.On();
    }

    public void HidePromptText()
    {
        po.Off();
    }
}
