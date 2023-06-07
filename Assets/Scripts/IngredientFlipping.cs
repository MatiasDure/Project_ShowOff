using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(IngredientHelper))]
public class IngredientFlipping : InteractableReaction
{
    public static event Action OnFlippingComplete;

    [SerializeField] float _requiredFlips;
    [SerializeField] float _timeLimit;

    List<Vector2> _inputHistory = new List<Vector2>();
    Vector2 _inputDir;

    float _deltaX;
    float _deltaY;
    float _oldX;
    float _oldY;

    int _maxInputHistory = 2;
    int _currFlips = 0;

    bool _startFlipping = false;

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
        if (DisgustQuest.Instance.QuestStep != DisgustQuest.QuestSteps.Flipping) return;

        _startFlipping = true;
        _helper.StartTask(_timeLimit);
    }

    void FixedUpdate()
    {
        if (!_startFlipping) return;

        FlippingHandler();
    }

    void FlippingHandler()
    {
        if (!_startFlipping) return;

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
                    if (IsCircularMotion() && _currFlips >= _requiredFlips)
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

        float deltaY = endPoint.y - startPoint.y;
        if (Mathf.Abs(deltaY) > 0)
        {
            if (Mathf.Sign(deltaY) != Mathf.Sign(startPoint.y))
            {
                _currFlips++;
                return true;
            }
        }

        //_currFlips = 0;
        return false;
    }

    void DoCircularMotion()
    {
        _currFlips = 0;
        FlippingComplete();
    }

    void FlippingComplete()
    {
        OnFlippingComplete?.Invoke();
        _startFlipping = false;
        _helper.EndTask();
    }

    void ResetCount()
    {
        _startFlipping = false;
        _currFlips = 0;
        _inputHistory.Clear();
    }

    void TimeUp()
    {
        ResetCount();
    }
}
