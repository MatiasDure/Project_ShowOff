using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : InteractableReaction
{
    [SerializeField] GameObject _toggleObj;
    [SerializeField] private bool _disableOnExit = true;

    protected override void Awake()
    {
        base.Awake();
        
        if(_toggleObj == null)
        {
            Debug.LogWarning("No GameObject passed to toggle!");
            Destroy(this);
        }
        
        if(_disableOnExit) InteractableScript.OnInteractableDeactivated += DeactivateObj;
    }
    
    private void ToggleObj() => _toggleObj.SetActive(!_toggleObj.activeInHierarchy);

    private void DeactivateObj() => _toggleObj.SetActive(false);

    protected override void Interact(InteractionInformation info) => ToggleObj();

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        if(_disableOnExit) InteractableScript.OnInteractableDeactivated -= DeactivateObj;
    }
}
