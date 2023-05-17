using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogInteraction : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _ps;

    [SerializeField]
    Color _lightCol;

    [SerializeField]
    int speedMultiplier;

    List<ParticleSystem.Particle> _enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> _exit = new List<ParticleSystem.Particle>();

    void OnParticleTrigger()
    {
        int numEnter = _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
        int numExit = _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, _exit);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle _p = _enter[i];
            _p.velocity *= speedMultiplier;

            if (i % 2 == 1)
            {
                _p.remainingLifetime = _p.remainingLifetime / 1.5f;
            }

            _enter[i] = _p;
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle _p = _exit[i];
            _exit[i] = _p;
        }

        // re-assign the modified particles back into the particle system
        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, _exit);
    }
}
