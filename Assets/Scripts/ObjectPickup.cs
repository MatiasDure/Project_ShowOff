using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : InteractableReaction
{
    [SerializeField] Vector3 _pickupPosition;

    protected override void Interact(InteractionInformation obj)
    {
        PickupObject();
    }

    void PickupObject()
    {
        GameObject _player = PlayerManager.Instance.gameObject;
        if (_player == null) return;

        transform.parent = _player.transform;
        transform.localPosition = _pickupPosition;

        base.InteractableScript.ForceExit();
        base.InteractableScript.enabled = false;
    }
}
