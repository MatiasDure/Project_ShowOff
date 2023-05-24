using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupManager : MonoBehaviour
{
    public static event Action<GameObject> OnIngredientCollected;

    List<GameObject> _objectsCollected = new List<GameObject>();

    void OnEnable()
    {
        Ingredient.OnIngredientPickup += CollectIngredient;
    }

    void OnDisable()
    {
        Ingredient.OnIngredientPickup -= CollectIngredient;
    }

    void CollectIngredient(GameObject _pObject)
    {
        if (DisgustQuest.state != LevelQuest.QuestState.InQuest) return;

        _objectsCollected.Add(_pObject);
        _pObject.GetComponent<Interactable>().ForceExit();
        _pObject.SetActive(false);

        OnIngredientCollected?.Invoke(_pObject);
    }
}
