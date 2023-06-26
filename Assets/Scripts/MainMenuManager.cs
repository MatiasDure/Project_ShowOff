using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuEssentials;

    public static MainMenuManager Instance;
    public bool firstTime = true;

    void OnEnable()
    {
        GameOverHandler.OnGameRestart += GameRestart;
    }

    void OnDisable()
    {
        GameOverHandler.OnGameRestart -= GameRestart;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (firstTime) EnableMenuEssentials();
    }

    void EnableMenuEssentials()
    {
        mainMenuEssentials.SetActive(true);
        firstTime = false;
    }

    public void DisableMenuEssentials()
    {
        mainMenuEssentials.SetActive(false);
    }

    void GameRestart()
    {
        firstTime = true;
    }
}
