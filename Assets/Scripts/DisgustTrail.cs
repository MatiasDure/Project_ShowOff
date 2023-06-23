using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class DisgustTrail : HintTrail
{
    void OnEnable()
    {
        DisgustQuest.OnShowNextHint += ShowNextHint;
    }

    void OnDisable()
    {
        DisgustQuest.OnShowNextHint -= ShowNextHint;
    }

    void ShowNextHint(Transform position)
    {
        ParticleSystem current = Instantiate(_trailParticle);
        _hintsParticles.Add(current);

        FollowPath particlePath = current.AddComponent<FollowPath>();
        particlePath.Target = position;
        particlePath.Speed = .1f;
        particlePath.StartingPosition = _player.transform.position;
        particlePath.ResetPosition();

        _particlesPath.Add(current, particlePath);
    }
}