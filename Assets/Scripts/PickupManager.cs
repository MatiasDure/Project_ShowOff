using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PickupManager : MonoBehaviour
{
    protected List<GameObject> _objectsCollected = new List<GameObject>();

    protected abstract void CollectObject(GameObject _pObject);
}
