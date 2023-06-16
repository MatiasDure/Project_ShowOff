using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public abstract class InteractableReaction : MonoBehaviour
{
    public event Action OnInteractableReaction;

    protected Interactable InteractableScript;
    protected bool _canInteract = true;

    protected virtual void Awake()
    {
        InteractableScript = GetComponent<Interactable>();
        InteractableScript.OnInteractableActivated += Interact;
    }

    protected virtual void Interact(InteractionInformation obj)
    {
        OnInteractableReaction?.Invoke();
    }

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
