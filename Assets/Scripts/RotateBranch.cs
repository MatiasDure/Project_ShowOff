using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class RotateBranch : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _branchPivot;

    private Interactable _interactable;

    private void Awake()
    {
        if (_branchPivot == null) Destroy(this);

        _interactable = GetComponent<Interactable>();

        _interactable.OnInteractableActivated += Interact;
    }

    private void Rotate(InteractionInformation info)
    {
        float angle = _branchPivot.transform.rotation.eulerAngles.x;

        angle = angle > 180 ? angle - 360 : angle;

        if(info.KeyPressed == KeyCode.LeftArrow &&
            angle < 45)
        {
            _branchPivot.transform.Rotate(new(2, 0, 0));
        }
        else if(info.KeyPressed == KeyCode.RightArrow &&
            angle > -45)
        {
            _branchPivot.transform.Rotate(new(-2, 0, 0));
        }
    }

    public void Interact(InteractionInformation info) => Rotate(info);

    private void OnDestroy()
    {
        _interactable.OnInteractableActivated -= Interact;
    }
}
