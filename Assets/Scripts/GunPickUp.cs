using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : PickUp
{
    [field: SerializeField] public Transform CurrentHoldingGun { get; set; }
}
