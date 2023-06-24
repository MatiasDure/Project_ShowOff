using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisgustToggleCamera : ToggleCamera
{
    public static event Action CameraInPlace;

    [SerializeField] SoundInteractable _poopInteractable;
    [SerializeField] DestroyInteractable _spiderInteractable;
    [SerializeField] float _toggleToPlayerSec;

    private bool _CanToggle = false;

    void OnEnable()
    {
        _poopInteractable.OnInteractableReaction += BeginToggleProcess;
        _spiderInteractable.OnSpiderWebDestroyed += BeginToggleProcess;
    }

    void OnDisable()
    {
        _poopInteractable.OnInteractableReaction -= BeginToggleProcess;
        _spiderInteractable.OnSpiderWebDestroyed -= BeginToggleProcess;
    }

    protected override bool ConditionToCheck() => _CanToggle;


    protected override void ResetAfterToggle()
    {
        _CanToggle = false;
    }

    void ToggleCam() => _CanToggle = true;

    void BeginToggleProcess()
    {
        ToggleCam();
        StartCoroutine(ToggleToPlayer(_toggleToPlayerSec));
    }

    IEnumerator ToggleToPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        ToggleCam();
    }
}
