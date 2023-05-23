using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupObject : InteractableReaction
{
    public static event Action<GameObject> OnObjectPickup;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected override void Interact(InteractionInformation info)
    {
        OnObjectPickup?.Invoke(this.gameObject);
    }
}
