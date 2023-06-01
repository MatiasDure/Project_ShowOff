using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToggleCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _camerasToToggleBetween;
    [SerializeField] private CameraMode[] _cameras;

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

        Debug.Log(firstCam.activeInHierarchy ? _cameras[0].Mode : _cameras[1].Mode);
    }
}

[System.Serializable]
public class CameraMode
{
    [SerializeField] private string _cameraMode;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public string Mode => _cameraMode;
    public CinemachineVirtualCamera VirtualCamera => _virtualCamera;
}
