using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlipping : MonoBehaviour
{
    enum Values
    {
        Up,
        Down,
        None
    }

    enum State
    {
        UnderThreshold,
        InThreshold
    }

    State _state = State.UnderThreshold;
    Values _lastValue = Values.None;
    Values _currValue = Values.None;

    [SerializeField] float _requiredPressSpeed = 10f;
    [SerializeField] float _spammingTimeSeconds = 0.5f;
    [SerializeField] float _requiredPressTime = 1f;

    float _pressValue = 0f;
    float _lastPressTime = 0f;
    float _startTime = 0f;

    int _pressCount = 0;

    Coroutine _lastRoutine = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _pressCount++;

            if(_pressCount == 1)
            {
                _startTime = Time.time;
                Debug.Log("Starting!");
            }
        }

        // Keep previous value
        if (_pressValue > 0)
        {
            _lastValue = Values.Up;
        }
        else if (_pressValue < 0)
        {
            _lastValue = Values.Down;
        }

        // Get current value
        _pressValue = Input.GetAxis("Vertical");

        // Current value
        if (_pressValue > 0)
        {
            _currValue = Values.Up;
        }
        else if (_pressValue < 0)
        {
            _currValue = Values.Down;
        }

        // If Up or Down was actually pressed
        if (_pressValue != 0)
        {
            _lastPressTime = Time.time;

            if (_currValue == _lastValue)
            {
                if (_lastRoutine != null) StopCoroutine(_lastRoutine);
                ResetValues();
                return;
            }

            float _pressSpeed = _pressCount / (Time.time - _startTime);
            if(_pressSpeed >= _requiredPressSpeed && _state != State.InThreshold)
            {
                _state = State.InThreshold;
                _lastRoutine = StartCoroutine(WithinThreshold());
            }
        }

        if(_pressCount > 1 && Time.time - _lastPressTime <= _requiredPressTime)
        {
            if (_lastRoutine != null) StopCoroutine(_lastRoutine);
            ResetValues();
            Debug.Log("Stopping!!!!");
        }
    }

    IEnumerator WithinThreshold()
    {
        yield return new WaitForSeconds(_spammingTimeSeconds);
        Debug.Log("Completed!");

        ResetValues();
    }

    void ResetValues()
    {
        _pressCount = 0;
        _startTime = 0f;
        _lastPressTime = 0.0f;
        _state = State.UnderThreshold;
        _lastValue = Values.None;
        _currValue = Values.None;
        Debug.Log("Resetting!");
    }
}
