using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Ammo))]
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TextMeshProUGUI _ammoText;

    private Ammo _ammo;

    private void Awake()
    {
        _ammo = GetComponent<Ammo>();

        _ammo.OnBulletCountChanged += ChangeText;

        ToggleCamera.OnCameraModeChanged += ToggleUI;
    }

    private void ChangeText(int pAmmoCount)
    {
        if (_ammoText != null) _ammoText.text = pAmmoCount.ToString();
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
