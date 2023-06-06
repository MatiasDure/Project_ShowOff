using System.Collections;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToActivate;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllObjects();
    }

    public void ActivateAllObjects()
    {
        for (int i = 0; i < _objectsToActivate.Length; i++)
        {
            _objectsToActivate[i].SetActive(true);
        }
    }

    public void DeactivateAllObjects()
    {
        for (int i = 0; i < _objectsToActivate.Length; i++)
        {
            _objectsToActivate[i].SetActive(false);
        }
    }
}
