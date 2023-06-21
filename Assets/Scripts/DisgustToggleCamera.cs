using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisgustToggleCamera : ToggleCamera
{
    public static event Action CameraInPlace;

    [SerializeField] SoundInteractable _interactable;

    private bool _CanToggle = false;

    void OnEnable()
    {
        _interactable.OnInteractableReaction += ToggleCam;
        _interactable.OnSoundPlayed += ToggleCam;
    }

    void OnDisable()
    {
        _interactable.OnInteractableReaction -= ToggleCam;
        _interactable.OnSoundPlayed += ToggleCam;
    }

    protected override bool ConditionToCheck() => _CanToggle;

    void ToggleCam() => _CanToggle = true;

    protected override void ResetAfterToggle()
    {
        _CanToggle = false;
    }
}
