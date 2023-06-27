using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractKeyVisualizer : MonoBehaviour
{
    [SerializeField] InteractionKeys[] _interactionKeys;
    [SerializeField] Image _keyImg;
    [SerializeField] GameObject _container;
    [SerializeField] Animator _animator;

    private Queue<InteractionKeys> _spritesWaitingForVisual = new Queue<InteractionKeys>(); 

    private void Awake()
    {
        Interactable.OnEnteredInteractionZone += FindInteractionKeySprite;
        Interactable.OnLeftInteractionZone += DisableVisualizer;
    }

    private void DisableVisualizer()
    {
        _container.SetActive(false);
        if (_spritesWaitingForVisual.Count > 0) SetInteractionVisual(_spritesWaitingForVisual.Dequeue());
    }

    private void DisableVisualizerAll()
    {
        _container.SetActive(false);
        _spritesWaitingForVisual.Clear();
    }

    private void Update()
    {
        if (GameState.Instance.IsFrozen) DisableVisualizerAll();
    }

    private void FindInteractionKeySprite(KeyCode[] pKeyCodes)
    {
        foreach(var key in _interactionKeys)
        {
            if (key.KeyChar != pKeyCodes[0]) continue;

            SetInteractionVisual(key);
            break;
        }
    }

    private void SetInteractionVisual(InteractionKeys pKey)
    {
        if (_keyImg == null || 
            pKey == null ||
            GameState.Instance.IsFrozen) return;

        if (VisualizerActive())
        {
            _spritesWaitingForVisual.Enqueue(pKey);
            return;
        }

        _keyImg.sprite = pKey.KeySprite;
        _animator.runtimeAnimatorController = pKey.Controller;
        EnableVisualizer();
    }

    private void EnableVisualizer() => _container.SetActive(true);

    private bool VisualizerActive() => _container.activeInHierarchy;

    private void OnDestroy()
    {
        Interactable.OnEnteredInteractionZone -= FindInteractionKeySprite;
        Interactable.OnLeftInteractionZone -= DisableVisualizer;
    }
}

[System.Serializable]
public class InteractionKeys
{
    [SerializeField] private Sprite _keySprite;
    [SerializeField] private KeyCode _keyChar;
    [SerializeField] private RuntimeAnimatorController _controller;

    public Sprite KeySprite => _keySprite;
    public KeyCode KeyChar => _keyChar;
    public RuntimeAnimatorController Controller => _controller;
}
