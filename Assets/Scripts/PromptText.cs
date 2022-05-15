using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptText : MonoBehaviour
{
    private Text textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<Text>();
        textMesh.enabled = false;
    }

    public void On()
    {
        textMesh.enabled = true;
    }


    public void Off()
    {
        textMesh.enabled = false;
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
