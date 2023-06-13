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

        _ammo.OnBulletCountChanged += ChangeText;
        _ammo.OnInfiteMode += InfiniteText;

        ToggleCamera.OnCameraModeChanged += ToggleUI;
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

    private void ToggleUI(string pCamMode)
    {
        _container.SetActive(pCamMode.Equals("Shooting"));
    }

    private void OnDestroy()
    {
        _ammo.OnBulletCountChanged -= ChangeText;

        ToggleCamera.OnCameraModeChanged -= ToggleUI;
    }
}
