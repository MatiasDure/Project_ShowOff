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
    [SerializeField] private float[] _yellowTimers;
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
                UpdateForState(State.Warning);
                break;
            case State.Warning:
                UpdateForState(State.Stop);
                AudioManager.instance.PlayWithPitch("Stop", 1f);
                break;
            case State.Stop:
                UpdateForState(State.Go);
                break;
            default:
                break;
        }

        OnTrafficStateChanged?.Invoke(_state);
    }

    private void UpdateForState(State pState)
    {
        _state = pState;
        _meshRenderer.material = _colors[(int)pState - 1];
        SetTimer(pState != State.Warning);
    }

    private void SetTimer(bool pRandom, float fromValue = 1, float toValue = 3)
    {
        _timer = pRandom ? UnityEngine.Random.Range(fromValue, toValue) : _yellowTimers[AngerQuest.Instance.InteractedCount];
    }
    private void SetRandomTimer(float fromInclusive, float toInclusive) => _timer = UnityEngine.Random.Range(fromInclusive, toInclusive);

    public void StartLights()
    {
        if (_state != State.None) return;

        UpdateForState(State.Go);
    }

    public void StopLights()
    {
        _state = State.None;
        _meshRenderer.material = _colors[3];
    }
}
