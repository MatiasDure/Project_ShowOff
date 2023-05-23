using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupManager : MonoBehaviour
{
    List<GameObject> _objectsCollected = new List<GameObject>();

    void OnEnable()
    {
        PickupObject.OnObjectPickup += CollectObject;
    }

    void OnDisable()
    {
        PickupObject.OnObjectPickup -= CollectObject;
    }

    void CollectObject(GameObject _pObject)
    {
        _objectsCollected.Add(_pObject);
        _pObject.SetActive(false);
    }
}
