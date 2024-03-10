using System.Collections.Generic;
using UnityEngine;

public class InteractiveDetector : MonoBehaviour
{
    public bool isInteractableInRange = false;

    public bool IsInteractableInRange
    {
        get { return isInteractableInRange; }
    }

    public Interactive interactiveObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider entered: " + other.gameObject.name);
        Interactive interactive = other.GetComponent<Interactive>();
        if (interactive != null)
        {
            isInteractableInRange = true;
            interactiveObject = interactive;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Collider exited: " + other.gameObject.name);
        Interactive interactive = other.GetComponent<Interactive>();
        if (interactive != null)
        {
            isInteractableInRange = false;
            interactiveObject = null;
        }
    }

}
