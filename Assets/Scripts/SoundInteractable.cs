using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundInteractable : InteractableReaction
{
    public event Action OnSoundPlayed;

    //[SerializeField] AudioClip _audioClip;
    [SerializeField] private string _soundToPlay;
    [SerializeField] float _audioDelaySec;
    [SerializeField] private string _audioMonsterInteraction;
    [SerializeField] float _audioMonsterInteractionTimer;


    protected override void Interact(InteractionInformation obj)
    {
        base.Interact(obj);
        CheckSound();
    }

    void CheckSound()
    {
        if(_soundToPlay == null)
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

        if(_audioMonsterInteractionTimer == 0)
        {
            PlayMonsterSound();
        }
        else
        {
            StartCoroutine(PlayWithDelayMonster(_audioMonsterInteractionTimer));
        }
    }

    void PlaySound()
    {
        // Play sound here
        AudioManager.instance.PlayWithPitch(_soundToPlay, 1f);
        Debug.Log("Playing sound!");
        OnSoundPlayed?.Invoke();
        _canInteract = true;
    }
    void PlayMonsterSound()
    {
        // Play sound here
        AudioManager.instance.PlayWithPitch(_audioMonsterInteraction, 1f);
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

    IEnumerator PlayWithDelayMonster(float delay)
    {
        _canInteract = false;
        yield return new WaitForSeconds(delay);
        PlayMonsterSound();
    }
}
