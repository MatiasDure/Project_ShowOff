using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomManager : MonoBehaviour
{
    [SerializeField] GameObject _hintGO;

    void OnEnable()
    {
    }

    void OnDisable()
    {
        MainMenuManager.Instance.OnGameStart -= EnableBedroomGOs;
    }

    void Start()
    {
        MainMenuManager.Instance.OnGameStart += EnableBedroomGOs;

        if (MainMenuManager.Instance.FirstTime) _hintGO.SetActive(false);
    }

    void EnableBedroomGOs()
    {
        _hintGO.SetActive(true);
    }
}
