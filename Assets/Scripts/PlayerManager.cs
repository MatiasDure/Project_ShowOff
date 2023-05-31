using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveBehaviour))]
public class PlayerManager : MonoBehaviour
{
    PlayerMoveBehaviour _moveBehaviour;

    

    void Awake()
    {
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
