using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum IngredientName
{
    Carrot,
    Onion,
    Potato
}

public class Ingredient : InteractableReaction
{
    public static event Action<GameObject> OnIngredientPickup;

    [field: SerializeField]
    public IngredientName IngrName { get; private set; }

    protected override void Interact(InteractionInformation obj)
    {
        AudioManager.instance.PlayWithPitch("Pickups", 1f);

        OnIngredientPickup?.Invoke(this.gameObject);
    }
}
