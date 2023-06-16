using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : InteractableReaction
{
    [SerializeField] Vector3 _pickupPosition;

    public bool PickedUp { get; private set; } = false;

    protected override void Interact(InteractionInformation obj)
    {
        AudioManager.instance.PlayWithPitch("Pickups", 1f);
        PickupObject();
    }

    void PickupObject()
    {
        GameObject _player = PlayerManager.Instance.gameObject;
        if (_player == null) return;

        transform.root.parent = _player.transform;
        transform.root.localPosition = _pickupPosition;

        base.InteractableScript.ForceExit();
        base.InteractableScript.enabled = false;

        PickedUp = true;
    }
}
