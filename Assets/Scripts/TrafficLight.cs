using System;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum State
    {
        None,
        Go,
        Warning,
        Stop
    }

    [SerializeField] private Material[] _colors;
    [SerializeField] private float[] _colorTimers;
    [SerializeField] private bool _randomizeTimer = true;
    [SerializeField] MeshRenderer _meshRenderer;   

    private State _state;

    private float _timer = 0f;

    public event Action<State> OnTrafficStateChanged;

    private bool TimerOver() => _timer <= 0f;

    private void Start()
    {
        _state = State.None;
    }

    private void Update()
    {
        if (_state == State.None || 
            GameState.Instance.IsFrozen) return;

        _timer -= Time.deltaTime;

        if (!TimerOver()) return;

        SwitchState();
    }

    private void SwitchState()
    {
        switch(_state)
        {
            case State.Go:
                _state = State.Warning;
                _timer = UnityEngine.Random.Range(1f, 3f);
                _meshRenderer.material = _colors[1];
                break;
            case State.Warning:
                _state = State.Stop;
                _timer = UnityEngine.Random.Range(1f, 3f);
                _meshRenderer.material = _colors[2];
                break;
            case State.Stop:
                _state = State.Go;
                _timer = UnityEngine.Random.Range(1f, 3f);
                _meshRenderer.material = _colors[0];
                break;
            default:
                break;
        }

        OnTrafficStateChanged?.Invoke(_state);
    }

    public void StartLights()
    {
        if (_state != State.None) return;

        _state = State.Go;
    }
}
