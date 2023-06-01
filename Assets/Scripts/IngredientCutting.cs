using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCutting : InteractableReaction, IRecipeStep
{
    enum State
    {
        UnderThreshold,
        InThreshold
    }

    State _state = State.UnderThreshold;

    [SerializeField] float _requiredPressSpeed = 10f;
    [SerializeField] float _spammingTimeSeconds = 0.5f;
    [SerializeField] float _requiredPressTime = 1f;

    float _lastPressTime = 0f;
    float _startTime = 0f;
    int _pressCount = 0;

    Coroutine _lastRoutine = null;

    protected override void Interact(InteractionInformation obj)
    {
        FreezeGameState();
    }

    public void FreezeGameState()
    {
        if (GameState.Instance.IsFrozen) return;

        GameState.Instance.IsFrozen = true;
    }

    public void UnFreezeGameState()
    {
        GameState.Instance.IsFrozen = false;
    }

    void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.E))
        {
            _pressCount++;

            if(_pressCount == 1)
            {
                _startTime = Time.time;
                Debug.Log("Starting!");
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            _lastPressTime = Time.time;

            if(_pressCount > 0)
            {
                float _pressSpeed = _pressCount / (Time.time - _startTime);
                Debug.Log(_pressSpeed);
                if(_pressSpeed >= _requiredPressSpeed && _state == State.UnderThreshold)
                {
                    _state = State.InThreshold;
                    _lastRoutine = StartCoroutine(WithinThreshold());
                }
                else if(_pressSpeed < _requiredPressSpeed && _state == State.InThreshold)
                {
                    StopCoroutine(_lastRoutine);
                    ResetCount();
                    Debug.Log("Stopping!!!!!");
                }
            }
        }

        if(_pressCount > 1 && Time.time - _lastPressTime >= _requiredPressTime)
        {
            if(_lastRoutine != null) StopCoroutine(_lastRoutine);
            ResetCount();
            Debug.Log("Stopping!!!!!");
        }
    }

    IEnumerator WithinThreshold()
    {
        yield return new WaitForSeconds(_spammingTimeSeconds);
        Debug.Log("Completed!");

        ResetCount();
    }

    void ResetCount()
    {
        _pressCount = 0;
        _startTime = 0.0f;
        _lastPressTime = 0.0f;
        _state =  State.UnderThreshold;
    }
}
