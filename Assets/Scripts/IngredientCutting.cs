using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(IngredientHelper))]
public class IngredientCutting : InteractableReaction
{
    enum State
    {
        UnderThreshold,
        InThreshold
    }

    public static event Action OnCuttingComplete;

    State _state = State.UnderThreshold;

    [Range(4,6)]
    [SerializeField] float _tempo;
    [SerializeField] float _spammingTimeSeconds = 0.5f;
    [SerializeField] float _timeLimit;

    [SerializeField] bool _skipTask = false;

    float _requiredPressTime = 1f;
    float _lastPressTime = 0f;
    float _startTime = 0f;
    int _pressCount = 0;

    bool _startCutting = false;

    Coroutine _lastRoutine = null;
    IngredientHelper _helper;

    void OnDisable()
    {
        _helper.OnTimerEnd -= TimeUp;
    }

    void Start()
    {
        _helper = GetComponent<IngredientHelper>();

        _helper.OnTimerEnd += TimeUp;
    }

    protected override void Interact(InteractionInformation obj)
    {
        if (_skipTask)
        {
            CuttingComplete();
            return;
        }
        if (DisgustQuest.Instance.QuestStep != DisgustQuest.QuestSteps.Cutting) return;
        AudioManager.instance.PlayWithPitch("TikTok", 1f);
        _startCutting = true;
        _helper.StartTask(_timeLimit);
    }

    void Update()
    {
        if (!_startCutting) return;

        CuttingHandler();
    }

    void CuttingHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _pressCount++;
            AudioManager.instance.PlayWithPitch("Chop", 1f);

            if (_pressCount == 1)
            {
                _startTime = Time.time;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            _lastPressTime = Time.time;

            if (_pressCount > 0)
            {
                float _pressSpeed = _pressCount / (Time.time - _startTime);
                if (_pressSpeed >= _tempo && _state == State.UnderThreshold)
                {
                    _state = State.InThreshold;
                    _lastRoutine = StartCoroutine(WithinThreshold());
                }
                else if (_pressSpeed < _tempo && _state == State.InThreshold)
                {
                    ResetCount();
                    Debug.Log("Stopping - Too slow!");
                }
            }
        }

        if (_pressCount > 1 && Time.time - _lastPressTime >= _requiredPressTime)
        {
            ResetCount();
            Debug.Log("Stopping - Too much delay between last press!");
        }
    }

    IEnumerator WithinThreshold()
    {
        yield return new WaitForSeconds(_spammingTimeSeconds);

        CuttingComplete();
    }

    void CuttingComplete()
    {
        AudioManager.instance.Stop("TikTok");
        AudioManager.instance.PlayWithPitch("Correct", 1f);
        OnCuttingComplete?.Invoke();
        _helper.EndTask();
        ResetCount();
    }

    void ResetCount()
    {
        if(_lastRoutine != null) StopCoroutine(_lastRoutine);

        _pressCount = 0;
        _startTime = 0.0f;
        _lastPressTime = 0.0f;
        _startCutting = false;
        _state =  State.UnderThreshold;
    }

    void TimeUp()
    {
        AudioManager.instance.Stop("TikTok");
        AudioManager.instance.PlayWithPitch("Timer", 1f);
        ResetCount();
    }
}
