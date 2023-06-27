using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToggleCamera : MonoBehaviour
{
    [SerializeField] protected CameraMode[] Cameras;
    [SerializeField] private CinemachineBrain _mainCamera;
    [SerializeField] private float _distanceThreshold;

    public static Action<string> OnCameraModeChanged;
    public static Action<CameraMode> OnReachedCameraMode;

    protected CameraMode CurrentCamera = null;
    protected bool ReachedNewCamera = true;

    public CameraMode CurrentCameraMode => CurrentCamera;
    public CameraMode PreviousCameraMode { get; private set; }
    public static ToggleCamera Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    protected virtual void Start()
    {
        SetPlayerCamAsCurrent();
    }

    private void SetPlayerCamAsCurrent()
    {
        foreach (var cam in Cameras)
        {
            if (cam.Mode != "PlayerCam") continue;

            CurrentCamera = cam;
            break;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (ConditionToCheck())
        {
            //toggle camera
            Toggle();
            ResetAfterToggle();
        }

        if (CurrentCamera == null || ReachedNewCamera) return;

        CheckDistanceToCurrentCam();
    }

    private void CheckDistanceToCurrentCam()
    {
        Vector3 currentCamPos = CurrentCamera.VirtualCamera.transform.position;
        Vector3 mainCamPos = _mainCamera.transform.position;
        Vector3 dir = currentCamPos - mainCamPos;
        float dist = dir.magnitude;

        if (dist < _distanceThreshold)
        {
            ReachedNewCamera = true;
            OnReachedCameraMode?.Invoke(CurrentCamera);
        }
    }

    protected abstract bool ConditionToCheck();

    protected virtual void ResetAfterToggle() { }

    //Only works for 2 cameras at the moment
    public void Toggle()
    {
        if (Cameras.Length < 2 ||
            Cameras[0] == null) return;

        GameObject firstCam = Cameras[0].VirtualCamera.gameObject;
        firstCam.SetActive(!firstCam.activeInHierarchy);
        Cameras[1].VirtualCamera.gameObject.SetActive(!firstCam.activeInHierarchy);

        PreviousCameraMode = CurrentCamera;
        CurrentCamera = firstCam.activeInHierarchy ? Cameras[0] : Cameras[1];

        OnCameraModeChanged?.Invoke(CurrentCamera.Mode);

        ReachedNewCamera = false;
    }

    public void SwitchCamera(string pCameraMode)
    {
        foreach(CameraMode mode in Cameras)
        {
            if (!mode.Mode.Equals(pCameraMode)) continue;

            //activating camera
            mode.VirtualCamera.gameObject.SetActive(false);
            mode.VirtualCamera.gameObject.SetActive(true);

            PreviousCameraMode = CurrentCamera;
            CurrentCamera = mode;
            ReachedNewCamera = false;
            OnCameraModeChanged?.Invoke(CurrentCamera.Mode);

            break;
        }
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
