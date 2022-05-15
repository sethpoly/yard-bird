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

    public void ShowPromptText(string text)
    {
        po.Setup();
        po.SetText(text);
        po.On();
    }

    public void HidePromptText()
    {
        string defaultText = "";
        po.SetText(defaultText);
        po.Off();
    }
}
