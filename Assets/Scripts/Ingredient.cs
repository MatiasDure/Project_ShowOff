using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum IngredientName
{
    Banana,
    Tomato,
    Apple
}

public class Ingredient : InteractableReaction
{
    public static event Action<GameObject> OnIngredientPickup;

    [field: SerializeField]
    public IngredientName _ingrName { get; private set; }

    protected override void Interact(InteractionInformation obj)
    {
        OnIngredientPickup?.Invoke(this.gameObject);
    }
}
