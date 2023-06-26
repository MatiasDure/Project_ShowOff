using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FearTreeCameraToggle : ToggleCamera
{
    private bool _CanToggle = false;

    void OnEnable()
    {
        FearEmotion.OnTreeCameraToggle += ToggleCam;
    }

    void OnDisable()
    {
        FearEmotion.OnTreeCameraToggle += ToggleCam;
    }

    protected override bool ConditionToCheck() => _CanToggle;

    void ToggleCam() => _CanToggle = true;

    protected override void ResetAfterToggle()
    {
        _CanToggle = false;
    }
}
