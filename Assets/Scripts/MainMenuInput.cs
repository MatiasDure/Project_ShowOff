using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInput : MonoBehaviour
{
    public void StartGame()
    {
        MainMenuManager.Instance.DisableMenuEssentials();
        GameState.Instance.IsFrozen = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application quitting!");
    }
}
