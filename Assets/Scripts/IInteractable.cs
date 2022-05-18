using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{   
    /// <summary>
    /// Attempt interaction with an object that conforms to this interface
    /// </summary>
    /// <param name = "interacter"> the object attempting an interaction with this
    /// <returns>
    /// A bool representing whether the interaction was successful
    /// </returns>
    public bool Interaction(GameObject interacter);

    public string GetInteractionPrompt();
}
