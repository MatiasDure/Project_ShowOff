using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteractable : InteractableReaction
{
    [Range(0,3f)]
    [SerializeField] float _fadePerSec;
    [SerializeField] private string _soundToPlay;
    [SerializeField] private string _soundToPlayMonster;

    bool _startFade = false;

    protected override void Interact(InteractionInformation obj)
    {
        if (!_canInteract) return;

        AudioManager.instance.PlayWithPitch(_soundToPlay, 1f);
        base.Interact(obj);
        DestroyGO();
        DisableInteractable();
    }

    void DestroyGO()
    {
        if (_fadePerSec == 0)
        {
            Destroy(gameObject);
            return;
        }

        _startFade = true;
    }

    void Update()
    {
        if (!_startFade) return;
        FadeGO();
    }

    void FadeGO()
    {
        Material _material = GetComponent<Renderer>().material;
        Color _color = _material.color;

        _color.a -= _fadePerSec * Time.deltaTime;
        _material.color = _color;

        if (_material.color.a <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlayWithPitch(_soundToPlayMonster, 1f);
        }
    }
}
