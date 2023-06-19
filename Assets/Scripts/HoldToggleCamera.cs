using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoldToggleCamera : ToggleCamera
{
    [SerializeField] private float _secondsToHold;
    [SerializeField] private KeyCode _keyToHold = KeyCode.E;

    private float _holdTimer;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        ToggleCamera.OnCameraModeChanged += PlaySound;
    }

    private void PlaySound(string pMode)
    {
        AudioManager.instance.PlayWithPitch(pMode.Equals("Shooting") ? "ZoomIn" : "ZoomOut", 1f);
    }
    // Update is called once per frame
    protected override void Update()
    {
        GameState.Instance.IsFrozen = IsShootMode();

        if (!Input.GetKey(_keyToHold))
        {
            _holdTimer = 0;
            return;
        }

        _holdTimer += Time.deltaTime;

        base.Update();
    }

    private bool IsShootMode()
    {
        foreach (var item in _cameras)
        {
            if (!item.Mode.Equals("Shooting")) continue;

            return item.VirtualCamera.gameObject.activeInHierarchy;
        }

        return false;
    }

    protected override bool ConditionToCheck() => CheckTimer();

    protected override void ResetAfterToggle()
    {
        _holdTimer = 0;
    }

    private bool CheckTimer() => _holdTimer >= _secondsToHold;

    private void OnDestroy()
    {
        ToggleCamera.OnCameraModeChanged -= PlaySound;
    }
}
