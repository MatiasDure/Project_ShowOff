using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToggleCamera : MonoBehaviour
{
    [SerializeField] protected CameraMode[] _cameras;

    public static Action<string> OnCameraModeChanged;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (ConditionToCheck())
        {
            //toggle camera
            Toggle();
            ResetAfterToggle();
        }
    }

    protected abstract bool ConditionToCheck();

    protected virtual void ResetAfterToggle() { }

    //Only works for 2 cameras at the moment
    private void Toggle()
    {
        if (_cameras.Length < 1 ||
            _cameras[0] == null) return;

        GameObject firstCam = _cameras[0].VirtualCamera.gameObject;
        firstCam.SetActive(!firstCam.activeInHierarchy);

        OnCameraModeChanged?.Invoke(firstCam.activeInHierarchy ? _cameras[0].Mode : _cameras[1].Mode);

        Debug.Log(firstCam.activeInHierarchy ? _cameras[0].Mode : _cameras[1].Mode);
    }
}

[System.Serializable]
public class CameraMode
{
    [SerializeField] private string _cameraMode;
    [SerializeField] private CinemachineVirtualCameraBase _virtualCamera;
    
    public string Mode => _cameraMode;
    public CinemachineVirtualCameraBase VirtualCamera => _virtualCamera;
}
