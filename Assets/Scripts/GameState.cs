using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool IsFrozen { get; set; }
    public bool IsPaused { get; set; }

    public static GameState Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

}
