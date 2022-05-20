using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptText : MonoBehaviour
{
    private Image textBackground;
    private Text textMesh;


    // Start is called before the first frame update
    void Start()
    {
        textBackground = GetComponentInParent<Image>();
        textMesh = GetComponent<Text>();
        textBackground.enabled = false;
        textMesh.enabled = false;
    }

    public void On()
    {
        textBackground.enabled = true;
        textMesh.enabled = true;
    }


    public void Off()
    {
        textMesh.text = "";
        textBackground.enabled = false;
        textMesh.enabled = false;
    }

    public void SetText(KeyCode keyCode, string text)
    {
        string keyCodeText = ((char)keyCode).ToString().ToUpper();
        textMesh.text = $"<color=#00ff45>{keyCodeText}</color> {text}";
    }
}
