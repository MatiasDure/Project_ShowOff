using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FearToggleCamera : ToggleCamera
{
    public static event Action CameraInPlace;

    private bool _CanToggle = false;

    void OnEnable()
    {
        FearQuest.OnFearQuestStart += ToggleCam;
        FearQuest.OnFearQuestEndTranstiion += ToggleCam;
    }

    void OnDisable()
    {
        FearQuest.OnFearQuestStart -= ToggleCam;
        FearQuest.OnFearQuestEndTranstiion -= ToggleCam;
    }

    protected override bool ConditionToCheck() => _CanToggle;

    void ToggleCam() => _CanToggle = true;

    protected override void ResetAfterToggle()
    {
        _CanToggle = false;
    }
}
