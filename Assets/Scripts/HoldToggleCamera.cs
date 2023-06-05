using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldToggleCamera : ToggleCamera
{
    [SerializeField] private float _secondsToHold;
    [SerializeField] private KeyCode _keyToHold = KeyCode.E;

    private float _holdTimer;

    // Update is called once per frame
    protected override void Update()
    {
        if (!Input.GetKey(_keyToHold))
        {
            _holdTimer = 0;
            return;
        }

        _holdTimer += Time.deltaTime;

        base.Update();
    }

    protected override bool ConditionToCheck() => CheckTimer();

    protected override void ResetAfterToggle()
    {
        _holdTimer = 0;
    }

    private bool CheckTimer() => _holdTimer >= _secondsToHold;
}
