using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour, IHittable
{
    public void Hit()
    {
        gameObject.SetActive(false);
    }
}
