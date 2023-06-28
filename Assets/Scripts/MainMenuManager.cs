using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuManager : MonoBehaviour
{
    public event Action OnGameStart;

    [SerializeField] GameObject mainMenuEssentials;

    public static MainMenuManager Instance;
    public bool FirstTime { get; private set; } = true;

    void OnEnable()
    {
        GameOverHandler.OnGameRestart += GameRestart;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        GameOverHandler.OnGameRestart -= GameRestart;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void EnableMenuEssentials()
    {
        mainMenuEssentials.SetActive(true);
        GameState.Instance.IsFrozen = true;
        FirstTime = false;
    }

    public void DisableMenuEssentials()
    {
        mainMenuEssentials.SetActive(false);
        OnGameStart?.Invoke();
    }

    void GameRestart()
    {
        FirstTime = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 0 && FirstTime)
        {
            EnableMenuEssentials();
        }
    }
}
