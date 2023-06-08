using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public abstract class InteractableReaction : MonoBehaviour
{
    protected Interactable InteractableScript;
    protected bool _canInteract = true;

    protected virtual void Awake()
    {
        InteractableScript = GetComponent<Interactable>();
        InteractableScript.OnInteractableActivated += Interact;
    }

    protected abstract void Interact(InteractionInformation obj);

    protected virtual void DisableInteractable()
    {
        _canInteract = false;
        InteractableScript.ForceExit();
        InteractableScript.enabled = false;
    }

    protected virtual void OnDestroy()
    {
        InteractableScript.OnInteractableActivated -= Interact;
    }
}
