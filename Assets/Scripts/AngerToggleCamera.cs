using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerToggleCamera : ToggleCamera
{
    private bool _CanToggle = false;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override bool ConditionToCheck() => _CanToggle;

    // Start is called before the first frame update
    void Start()
    {
        MonsterNavMesh.OnReachedNewPosition += ToggleCam;
        AngerQuest.OnTouchedMonster += ToggleCam;
    }

    private void ToggleCam() => _CanToggle = true;

    protected override void ResetAfterToggle()
    {
        _CanToggle = false;
    }

    private void OnDestroy()
    {
        MonsterNavMesh.OnReachedNewPosition -= ToggleCam;
        AngerQuest.OnTouchedMonster -= ToggleCam;
    }
}
