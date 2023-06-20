using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundInteractable : InteractableReaction
{
    public event Action OnSoundPlayed;

    [SerializeField] AudioClip _audioClip;
    [SerializeField] float _audioDelaySec;

    protected override void Interact(InteractionInformation obj)
    {
        base.Interact(obj);
        CheckSound();
    }

    void CheckSound()
    {
        if(_audioClip == null)
        {
            Debug.Log("Audio clip not found!");
            return;
        }

        if(_audioDelaySec == 0)
        {
            PlaySound();
        }
        else
        {
            StartCoroutine(PlayWithDelay(_audioDelaySec));
        }
    }

    void PlaySound()
    {
        // Play sound here
        Debug.Log("Playing sound!");
        OnSoundPlayed?.Invoke();
        _canInteract = true;
    }

    IEnumerator PlayWithDelay(float delay)
    {
        _canInteract = false;
        yield return new WaitForSeconds(delay);
        PlaySound();
    }
}
