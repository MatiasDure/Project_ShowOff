using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadnessTerrain : MonoBehaviour
{
    MeshRenderer _rend;

    void Awake()
    {
        _rend = GetComponent<MeshRenderer>();
        Color c = _rend.material.color;
        c.a = 0;
        _rend.material.color = c;
    }
}
