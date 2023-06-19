using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ammo))]
public class Gun : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = .2f;

    private Ammo _ammo;
    private bool _ignoredFirst = false;

    LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        _ammo = GetComponent<Ammo>();
    }

    private void Start()
    {
        ToggleCamera.OnCameraModeChanged += CheckResetAim;
    }

    private void Update()
    {
        //not in shooting mode
        if (!GameState.Instance.IsFrozen)
        {
            lineRenderer.enabled = false;
            _ignoredFirst = false;
            return;
        }

        lineRenderer.enabled = true;
        Vector3 lineEnd = transform.position + transform.forward * 20f;
        lineRenderer.SetPositions(new Vector3[] { transform.position, lineEnd });

        if (Input.GetKeyUp(KeyCode.E))
        {
            if(_ignoredFirst) Shoot();

            _ignoredFirst = true;
        }

        Aim();
    }

    private void Aim()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
               
        transform.Rotate(Vector3.up, horizontal * _rotateSpeed);
        transform.Rotate(Vector3.right * -1, vertical * _rotateSpeed);

        float angleX = transform.localRotation.eulerAngles.x;
        float angleY = transform.localRotation.eulerAngles.y;

        float newX = angleX > 180 ? angleX - 360 : angleX;
        float newY = angleY > 180 ? angleY - 360 : angleY;

        newX = Mathf.Clamp(newX, -22f, 2f);
        newX = newX < 0 ? 360 - newX * -1 : newX;

        newY = Mathf.Clamp(newY, -37f, 37f);
        newY = newY < 0 ? 360 - newY * -1 : newY;

        Quaternion rotation = new();
        rotation.eulerAngles = new(newX, newY, 0);

        transform.localRotation = rotation;
    }

    private void ResetAim()
    {
        Quaternion rotation = new();
        rotation.eulerAngles = new(0, 0, 0);

        transform.localRotation = rotation;
    }

    private void CheckResetAim(string pMode)
    {
        if (pMode != "Shooting") ResetAim();
    }

    private void Shoot()
    {
        bool currentlyInQuest = SadnessQuest.Instance.CurrentQuestState == LevelQuest.QuestState.InQuest;

        if (!_ammo.AmmoAvailable &&
            currentlyInQuest) return;
        AudioManager.instance.PlayWithPitch("Spray", 1f);
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

        foreach (RaycastHit hit in hits)
        {
            if (!hit.transform.TryGetComponent<IHittable>(out IHittable hittable)) continue;

            hittable.Hit();
        }

        if(currentlyInQuest) _ammo.ModifyBulletCount();
    }

    private void OnDestroy()
    {
        ToggleCamera.OnCameraModeChanged -= CheckResetAim;
    }
}
