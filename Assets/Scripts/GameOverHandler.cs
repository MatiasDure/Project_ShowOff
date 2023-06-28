using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverHandler : MonoBehaviour
{
    public static event Action OnGameRestart;

    [SerializeField] private KeyCode _keyToCheck;

    void Update()
    {
        if (Input.GetKeyDown(_keyToCheck))
        {
            OnGameRestart?.Invoke();
            LoadingScene.Instance.LoadScene(0);
        }
    }
}
