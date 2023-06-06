using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _ammoCount;
    public int AmountCount { get; private set; }
    public bool AmmoAvailable { get; private set; }

    public Action<int> OnBulletCountChanged;

    public void Start()
    {
        ResetAmmo();
    }

    public void ModifyBulletCount(int pModifyBy = -1)
    {
        AmountCount += pModifyBy;
        OnBulletCountChanged?.Invoke(AmountCount);
        CheckAmmoAvailable();
    }

    private void ResetAmmo()
    {
        AmountCount = _ammoCount;
        CheckAmmoAvailable();
    }

    private void CheckAmmoAvailable() => AmmoAvailable = AmountCount > 0;
}
