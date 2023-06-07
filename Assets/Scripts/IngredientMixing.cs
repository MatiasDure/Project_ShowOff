using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(IngredientHelper))]
public class IngredientMixing : InteractableReaction
{
    public static event Action OnMixingComplete;

    [SerializeField] float _requiredRotations;
    [SerializeField] float _timeLimit;
    [SerializeField] bool _skipTask = false;

    List<Vector2> _inputHistory = new List<Vector2>();
    Vector2 _inputDir;

    float _deltaX;
    float _deltaY;
    float _oldX;
    float _oldY;

    int _maxInputHistory = 2;
    int _currRotations = 0;

    bool _startMixing = false;

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
            MixingComplete();
            return;
        }
        if (DisgustQuest.Instance.QuestStep != DisgustQuest.QuestSteps.Mixing) return;

        _startMixing = true;
        _helper.StartTask(_timeLimit);
    }

    void FixedUpdate()
    {
        if (!_startMixing) return;
        MixingHandler();
    }

    void MixingHandler()
    {
        _deltaX = Input.GetAxisRaw("Horizontal") - _oldX;
        _deltaY = Input.GetAxisRaw("Vertical") - _oldY;
        _oldX = Input.GetAxisRaw("Horizontal");
        _oldY = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(_deltaX) < 1 && Mathf.Abs(_deltaY) < 1)
        {
            _inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _inputHistory.Clear();
        }

        if (Mathf.Abs(_deltaX) >= 1 || Mathf.Abs(_deltaY) >= 1)
        {
            _inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") == _inputDir.x && Input.GetAxisRaw("Vertical") == _inputDir.y)
            {
                _inputHistory.Add(_inputDir);

                if (_inputHistory.Count > _maxInputHistory)
                {
                    _inputHistory.RemoveAt(0);
                }

                if (_inputHistory.Count == _maxInputHistory)
                {
                    if (IsCircularMotion() && _currRotations >= _requiredRotations)
                    {
                        DoCircularMotion();
                    }
                }
            }
            else
            {
                _inputHistory.Clear();
            }
        }
    }

    bool IsCircularMotion()
    {
        Vector2 startPoint = _inputHistory[0];
        Vector2 endPoint = _inputHistory[_maxInputHistory - 1];

        float angle = Vector2.SignedAngle(startPoint, endPoint);
        if (Mathf.Abs(angle) >= 315f || Mathf.Abs(angle) <= 45f && angle != 0f)
        {
            _currRotations++;
            return true;
        }

        _currRotations = 0;
        return false;
    }

    void DoCircularMotion()
    {
        _currRotations = 0;

        MixingComplete();
    }

    void MixingComplete()
    {
        OnMixingComplete?.Invoke();
        _startMixing = false;
        _helper.EndTask();
    }

    void ResetCount()
    {
        _startMixing = false;
        _currRotations = 0;
        _inputHistory.Clear();
    }

    void TimeUp()
    {
        ResetCount();
    }
}
