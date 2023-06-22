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
        GameObject player = PlayerManager.Instance.gameObject;
        GameObject plate = transform.root.gameObject;
        if (player == null) return;

        plate.transform.SetParent(player.transform);
        plate.transform.localPosition = _pickupPosition;

        base.InteractableScript.ForceExit();
        base.InteractableScript.enabled = false;

        PickedUp = true;
    }
}
