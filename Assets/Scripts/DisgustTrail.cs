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
        bool newPS = true;
        _positionsToGo[0] = position;

        ParticleSystem current;

        if (_hintsParticles.Count == 0)
        {
            return;
        }
        else
        {
            current = _hintsParticles[0];
            newPS = false;
        }
        current.Play();

        FollowPath particlePath;
        if (newPS) particlePath = current.AddComponent<FollowPath>();
        else particlePath = current.GetComponent<FollowPath>();

        particlePath.Target = _positionsToGo[0];
        particlePath.Speed = .1f;
        particlePath.StartingPosition = _player.transform.position;
        particlePath.ResetPosition();

        if (newPS) _particlesPath.Add(current, particlePath);
    }
}
