using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HoldToggleCamera : ToggleCamera
{
    [SerializeField] private float _secondsToHold;
    [SerializeField] private KeyCode _keyToHold = KeyCode.E;
    [SerializeField] private Slider _slider;

    private float _holdTimer;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        ToggleCamera.OnCameraModeChanged += CameraModeChanged;
    }

    private void CameraModeChanged(string pCurrentMode)
    {
        PlaySound(pCurrentMode);
    }

    private void PlaySound(string pMode)
    {
        AudioManager.instance.PlayWithPitch(pMode.Equals("Shooting") ? "ZoomIn" : "ZoomOut", 1f);
    }

    protected override void Update()
    {
        base.Update();

        GameState.Instance.IsFrozen = ToggleCamera.Instance.CurrentCameraMode == null ? false : ToggleCamera.Instance.CurrentCameraMode.Mode == "Shooting";

        if (!Input.GetKey(_keyToHold))
        {
            ResetHold();
            return;
        }

        _holdTimer += Time.deltaTime;
        _slider.value = _holdTimer / _secondsToHold;

        ChangeCam();
    }

    private void ResetHold()
    {
        _holdTimer = 0;
        _slider.value = 0;
    }

    protected override bool ConditionToCheck() => false;

    private void ChangeCam()
    {
        if (!CheckTimer()) return;

        ResetHold();
        SwitchCamera(CurrentCamera.Mode == "PlayerCam" ? "Shooting" : "PlayerCam");
    }

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
