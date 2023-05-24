using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupManager : MonoBehaviour
{
    List<GameObject> _objectsCollected = new List<GameObject>();

    void OnEnable()
    {
        Ingredient.OnObjectPickup += CollectObject;
    }

    void OnDisable()
    {
        Ingredient.OnObjectPickup -= CollectObject;
    }

    void CollectObject(GameObject _pObject)
    {
        if (DisgustQuest.state != LevelQuest.QuestState.InQuest) return;

        _objectsCollected.Add(_pObject);
        _pObject.GetComponent<Interactable>().ForceExit();
        _pObject.SetActive(false);
    }
}
