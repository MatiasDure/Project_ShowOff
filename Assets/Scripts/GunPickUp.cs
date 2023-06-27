using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : PickUp
{
    [SerializeField]private GunModel[] _gunModels;
    [field: SerializeField] public GunModel CurrentHoldingGun { get; set; }

    public void Toggle(GunModel.Types pModel)
    {
        foreach(var gun in _gunModels)
        {
            if (!gun.ModelType.Equals(pModel)) continue;

            CurrentHoldingGun = gun;
            break;
        }
    }
}
