using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IngredientPickup : PickupManager
{
    [SerializeField] DisgustQuest _disgustQuest;

    public static event Action<GameObject> OnIngredientCollected;
    public static event Action OnAllIngredientsCollected;

    void OnEnable()
    {
        Ingredient.OnIngredientPickup += CollectIngredient;
    }

    void OnDisable()
    {
        Ingredient.OnIngredientPickup -= CollectIngredient;
    }

    protected override void CollectIngredient(GameObject _pObject)
    {
        if (DisgustQuest.state != DisgustQuest.QuestState.InQuest) return;

        _objectsCollected.Add(_pObject);
        _pObject.GetComponent<Interactable>().ForceExit();
        _pObject.SetActive(false);

        OnIngredientCollected?.Invoke(_pObject);


        if(_objectsCollected.Count == _disgustQuest.IngredientsToPickup.Count)
        {
            OnAllIngredientsCollected?.Invoke();
        }
    }
}
