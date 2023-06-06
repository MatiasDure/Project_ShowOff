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

        _ammo.OnBulletCountChanged += (int pAmmoCount) => 
        {
            if(_ammoText != null) _ammoText.text = pAmmoCount.ToString();
        };

        ToggleCamera.OnCameraModeChanged += (string pCamMode) =>
        {
            _container.SetActive(pCamMode.Equals("Shooting"));
        };
    }
}
