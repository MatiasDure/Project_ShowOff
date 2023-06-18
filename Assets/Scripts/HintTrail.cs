using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HintTrail : MonoBehaviour
{
    [SerializeField] private Transform[] _positionsToGo;
    [SerializeField] ParticleSystem _trailParticle;
    [SerializeField] PlayerMoveBehaviour _player;
    [SerializeField] float _secondsForHint;

    private List<ParticleSystem> _hintsParticles = new List<ParticleSystem>();
    private Dictionary<ParticleSystem, FollowPath> _particlesPath = new Dictionary<ParticleSystem, FollowPath>();
    private float _lastHintProvided = 0;

    void Awake()
    {
        if (_player == null) _player = FindObjectOfType<PlayerMoveBehaviour>();

        if (_player == null) throw new Exception("No player in the level");
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _positionsToGo.Length; i++)
        {
            ParticleSystem current = Instantiate(_trailParticle);
            _hintsParticles.Add(current);

            FollowPath particlePath = current.AddComponent<FollowPath>();
            particlePath.Target = _positionsToGo[i];
            particlePath.Speed = .1f;
            particlePath.StartingPosition = _player.transform.position;
            particlePath.ResetPosition();

            _particlesPath.Add(current, particlePath);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool _allParticlesDisabled = true;

        foreach(var particle in _hintsParticles)
        {
            FollowPath current = _particlesPath[particle];

            if (!current.ReachedTarget)
            {
                _allParticlesDisabled = false;
                continue;
            }
            
            particle.Stop();
        }

        if(_allParticlesDisabled && HintsNeeded()) EnableHints();
    }

    private bool HintsNeeded()
    {
        _lastHintProvided += Time.deltaTime;

        if (_player.IsMoving)
        {
            _lastHintProvided = 0;
            return false;
        }

        return _lastHintProvided >= _secondsForHint;
    }

    private void EnableHints()
    {
        foreach (var particle in _hintsParticles)
        {
            FollowPath current = _particlesPath[particle];

            if (!current.ReachedTarget) continue;

            current.StartingPosition = _player.transform.position;
            current.ResetPosition();
            particle.Play();
        }

        _lastHintProvided = 0;
    }
}
