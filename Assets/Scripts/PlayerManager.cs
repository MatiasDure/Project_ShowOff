using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMoveBehaviour))]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    PlayerMoveBehaviour _moveBehaviour;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("More than one PlayerManager instance in scene!");

        _moveBehaviour = GetComponent<PlayerMoveBehaviour>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameState.Instance.IsFrozen) return;

        PlayerMovement();
    }

    void PlayerMovement()
    {
        _moveBehaviour.Move();
    }
}
