using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ToggleObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _toggleObj;
    [SerializeField] private bool _disableOnExit = true;

    private Interactable _interactableScript;

    private void Awake()
    {
        if(_toggleObj == null)
        {
            Debug.LogWarning("No GameObject passed to toggle!");
            Destroy(this);
        }

        _interactableScript = GetComponent<Interactable>();

        _interactableScript.OnInteractableActivated += Interact;
        
        if(_disableOnExit) _interactableScript.OnInteractableDeactivated += DeactivateObj;
    }
    
    private void ToggleObj(InteractionInformation info) => _toggleObj.SetActive(!_toggleObj.activeInHierarchy);

    private void DeactivateObj() => _toggleObj.SetActive(false);

    public void Interact(InteractionInformation info) => ToggleObj(info);

    private void OnDestroy()
    {
        _interactableScript.OnInteractableActivated -= Interact;
        
        if(_disableOnExit) _interactableScript.OnInteractableDeactivated -= DeactivateObj;
    }
}
