using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IngredientPickup : PickupManager
{
    public static event Action<GameObject> OnIngredientCollected;
    public static event Action OnAllIngredientsCollected;

    void OnEnable()
    {
        Ingredient.OnIngredientPickup += CollectObject;
    }

    void OnDisable()
    {
        Ingredient.OnIngredientPickup -= CollectObject;
    }

    protected override void CollectObject(GameObject _pObject)
    {
        if (DisgustQuest.Instance.State != DisgustQuest.QuestState.InQuest) return;

        AudioManager.instance.PlayWithPitch("Pickups", 1f);

        _objectsCollected.Add(_pObject);
        _pObject.GetComponent<Interactable>().ForceExit();
        _pObject.SetActive(false);

        OnIngredientCollected?.Invoke(_pObject);

        if(_objectsCollected.Count == DisgustQuest.Instance.IngredientsToPickup.Count)
        {
            OnAllIngredientsCollected?.Invoke();
        }
    }
}
