using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [field: SerializeField] public Transform PickUpJoint { get; private set; }
}
