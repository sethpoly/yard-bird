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
        
        Skewerable skewerable = interacter.GetComponent<Skewerable>();
        skewerable.SetIdle();
        skewerable.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);

        return true;
    }

    public string GetInteractionPrompt()
    {
        return "";
    }
}
