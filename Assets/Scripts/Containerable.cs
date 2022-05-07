using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObjects that are able to "hold" other objects, 
/// this is useful for garbage bins, dumpsters, etc.
/// </summary>
public class Containerable : MonoBehaviour, IInteractable
{
    public bool Interaction(GameObject interacter) 
    {
        Debug.Log("Trying to interact with Containerable object with " + interacter.name);

        // TODO: place the *iteracter* object at x,y,z of this gameObject
        // It fill "fall into place"

        //interacter.transform.position = transform.position;
        

        return true;
    }
}
