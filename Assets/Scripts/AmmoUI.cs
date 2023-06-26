using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Ammo))]
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private GameObject _infiniteImg;

    private Ammo _ammo;

    private void Awake()
    {
        _ammo = GetComponent<Ammo>();
    }

    private void Start()
    {
        _ammo.OnBulletCountChanged += ChangeText;
        _ammo.OnInfiniteMode += InfiniteText;

        ToggleCamera.OnReachedCameraMode += ToggleUI;
    }

    private void ChangeText(int pAmmoCount)
    {
        if (_ammoText != null) _ammoText.text = pAmmoCount.ToString();
    }

    private void InfiniteText(bool pEnabled)
    {
        if (_infiniteImg != null) _infiniteImg.SetActive(pEnabled);
        if (_ammoText != null) _ammoText.gameObject.SetActive(!pEnabled);
    }

    private void ToggleUI(CameraMode pCamMode)
    {
        _container.SetActive(pCamMode.Mode.Equals("Shooting"));
    }

    private void OnDestroy()
    {
        _ammo.OnBulletCountChanged -= ChangeText;
        //_ammo.OnInfiniteMode -= InfiniteText;

        ToggleCamera.OnReachedCameraMode -= ToggleUI;
    }
}
