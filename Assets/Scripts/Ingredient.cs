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
        OnIngredientPickup?.Invoke(this.gameObject);
    }
}
