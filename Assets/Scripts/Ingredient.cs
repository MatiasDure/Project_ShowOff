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
    public static event Action<GameObject> OnObjectPickup;

    [SerializeField] IngredientName _ingredient;

    protected override void Interact(InteractionInformation obj)
    {
        OnObjectPickup?.Invoke(this.gameObject);
    }
}
