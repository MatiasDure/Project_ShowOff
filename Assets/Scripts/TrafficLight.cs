using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TrafficLight : MonoBehaviour
{
    public enum State
    {
        None,
        Go,
        Warning,
        Stop
    }

    //[SerializeField] private CinemachineFreeLook _camera;
    //[SerializeField] private Material[] _colors;
    [SerializeField] private float[] _yellowTimers;
    [SerializeField] private Volume _globalVolume;

    //private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
    private Vignette _vignette;

    private State _state;

    private float _timer = 0f;

    public event Action<State> OnTrafficStateChanged;

    private bool TimerOver() => _timer <= 0f;

    private void Start()
    {
        _globalVolume.profile.TryGet<Vignette>(out _vignette);

        //if (_multiChannelPerlin == null) _multiChannelPerlin = _camera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _state = State.None;
    }

    private void Update()
    {
        //if (_vignette.intensity.value > 0) _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0, .01f);

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
                AudioManager.instance.PlayWithPitch("Warning", 1f);
                _vignette.color.value = Color.yellow;
                break;
            case State.Warning:
                UpdateForState(State.Stop);
                AudioManager.instance.PlayWithPitch("Stop", 1f);
                _vignette.color.value = Color.red;
                break;
            case State.Stop:
                UpdateForState(State.Go);
                AudioManager.instance.PlayWithPitch("Start", 1f);
                _vignette.color.value = Color.green;
                break;
            default:
                break;
        }

        _vignette.intensity.value = 1f;
        OnTrafficStateChanged?.Invoke(_state);
    }

    private void UpdateForState(State pState)
    {
        _state = pState;
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

        OnTrafficStateChanged?.Invoke(_state);
    }

    public void StopLights()
    {
        _state = State.None;

        OnTrafficStateChanged?.Invoke(_state);
    }
}
