using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class FearTrail : HintTrail
{
    Coroutine currRoutine = null;

    void OnEnable()
    {
        FearQuest.OnTorchLit += BeginHintCountdown;
    }

    void OnDisable()
    {
        FearQuest.OnTorchLit -= BeginHintCountdown;
    }

    void ShowNextHint(Transform position)
    {
        Debug.Log("Showing!");
        bool newPS = true;
        _positionsToGo[0] = position;

        ParticleSystem current;

        if (_hintsParticles.Count == 0)
        {
            current = Instantiate(_trailParticle);
            _hintsParticles.Add(current);
        }
        else
        {
            current = _hintsParticles[0];
            newPS = false;
        }

        FollowPath particlePath;
        if (newPS) particlePath = current.AddComponent<FollowPath>();
        else particlePath = current.GetComponent<FollowPath>();

        current.Play();

        particlePath.Target = _positionsToGo[0];
        particlePath.Speed = .1f;
        particlePath.StartingPosition = _player.transform.position;
        particlePath.ResetPosition();

        if(newPS) _particlesPath.Add(current, particlePath);
    }

    void BeginHintCountdown(Transform position)
    {
        if(currRoutine != null) StopCoroutine(currRoutine);

        currRoutine = StartCoroutine(HintCountdown(position));
    }

    IEnumerator HintCountdown(Transform position)
    {
        yield return new WaitForSeconds(_secondsForHint);
        ShowNextHint(position);
        BeginHintCountdown(position); // Repeat
    }
}
