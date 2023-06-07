using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IngredientHelper : MonoBehaviour
{
    public event Action OnTimerEnd;

    float _taskTimer = 0f;

    bool _startTimer = false;

    public void StartTask(float pTimeLimit)
    {
        _taskTimer = pTimeLimit;
        _startTimer = true;

        FreezeGameState();
    }

    public void EndTask()
    {
        UnFreezeGameState();
        _startTimer = false;
    }

    void FreezeGameState()
    {
        if (GameState.Instance == null) return;

        GameState.Instance.IsFrozen = true;
    }

    void UnFreezeGameState()
    {
        if (GameState.Instance == null) return;

        GameState.Instance.IsFrozen = false;
    }

    void CountDown()
    {
        if (_taskTimer > 0)
        {
            _taskTimer -= Time.deltaTime;
            Debug.Log($"Time left: {(int)_taskTimer}");
        }
        else
        {
            OnTimerEnd?.Invoke();
            UnFreezeGameState();
            _startTimer = false;
            Debug.Log("Time up, try again!");
        }
    }

    void Update()
    {
        if (!_startTimer) return;

        CountDown();
    }
}
