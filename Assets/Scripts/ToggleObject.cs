using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ToggleObject : MonoBehaviour
{
    [SerializeField] private GameObject _toggleObj;

    private Interactable _interactableScript;

    private void Awake()
    {
        if(_toggleObj == null)
        {
            Debug.LogWarning("No GameObject passed to toggle!");
            Destroy(this);
        }

        _interactableScript = GetComponent<Interactable>();

        _interactableScript.OnInteractableActivated += ToggleObj;
        _interactableScript.OnInteractableDeactivated += DeactivateObj;
    }
    
    private void ToggleObj() => _toggleObj.SetActive(!_toggleObj.activeInHierarchy);

    private void DeactivateObj() => _toggleObj.SetActive(false);
}
