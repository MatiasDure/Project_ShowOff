using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltShaker : MonoBehaviour
{
    [SerializeField] Animator _animation;
    [SerializeField] ParticleSystem _ps;

    public void DropSalt()
    {
        _ps.Play();
    }
}
