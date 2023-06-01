using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IngredientFlipping : InteractableReaction, IRecipeStep
{
    public static event Action OnFlippingComplete;

    [SerializeField] float _requiredFlips;

    List<Vector2> _inputHistory = new List<Vector2>();
    Vector2 _inputDir;

    float _deltaX;
    float _deltaY;
    float _oldX;
    float _oldY;

    int _maxInputHistory = 2;
    int _currFlips = 0;

    bool _startFlipping = false;

    protected override void Interact(InteractionInformation obj)
    {
        if (DisgustQuest.Instance.QuestStep != DisgustQuest.QuestSteps.Flipping) return;

        _startFlipping = true;
        FreezeGameState();
    }

    public void FreezeGameState()
    {
        if (GameState.Instance.IsFrozen) return;

        GameState.Instance.IsFrozen = true;
    }

    public void UnFreezeGameState()
    {
        GameState.Instance.IsFrozen = false;
    }

    void Update()
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
                        // Perform your action for circular motion here
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
        Debug.Log(deltaY);
        if (Mathf.Abs(deltaY) >= 0.5f)
        {
            if (Mathf.Sign(deltaY) != Mathf.Sign(startPoint.y))
            {
                _currFlips++;
                return true;
            }
        }

        _currFlips = 0;
        return false;
    }

    void DoCircularMotion()
    {
        _currFlips = 0;
        FlippingComplete();
    }

    void FlippingComplete()
    {
        Debug.Log("AYYYYYY");

        OnFlippingComplete?.Invoke();
        UnFreezeGameState();
    }
}
