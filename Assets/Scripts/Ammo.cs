using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _ammoCount;
    public int AmountCount { get; private set; }
    public bool AmmoAvailable { get; private set; }

    private bool _infiniteMode = false;

    public Action<int> OnBulletCountChanged;
    public Action<bool> OnInfiniteMode;

    public void Start()
    {
        ResetAmmo();
        EnableInfiniteAmmo();
    }

    public void ModifyBulletCount(int pModifyBy = -1)
    {
        if (_infiniteMode) return;

        AmountCount += pModifyBy;
        OnBulletCountChanged?.Invoke(AmountCount);
        CheckAmmoAvailable();
    }

    private void ResetAmmo()
    {
        AmountCount = _ammoCount;
        OnBulletCountChanged?.Invoke(AmountCount);
        CheckAmmoAvailable();
    }

    public void EnableInfiniteAmmo()
    {
        _infiniteMode = true;
        OnInfiniteMode?.Invoke(true);
    }

    public void DisableInfiniteAmmo()
    {
        _infiniteMode = false;
        OnInfiniteMode?.Invoke(false);
        ResetAmmo();
    }

    private void CheckAmmoAvailable() => AmmoAvailable = AmountCount > 0;
}
