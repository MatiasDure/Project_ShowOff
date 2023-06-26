using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheck : MonoBehaviour
{
    [SerializeField] private KeyCode _keyToCheck;

    private void Awake()
    {
        GameState.Instance.IsFrozen = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyToCheck))
        {
            GameState.Instance.IsFrozen = false;
            MainMenuManager.Instance.DisableMenuEssentials();
        }
    }
}
