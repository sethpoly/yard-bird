using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skewerable : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction() 
    {
        Debug.Log("Trying to interact with a Skewerable object!");
    }
}
