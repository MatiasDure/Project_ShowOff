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
    [SerializeField] private MonsterEmotion _monsterEmotion;


    protected override void Interact(InteractionInformation obj)
    {
        if (!_canInteract) return;

        base.Interact(obj);
        _canInteract = false;
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
        OnSoundPlayed?.Invoke();
    }
    void PlayMonsterSound()
    {
        // Play sound here
        _canInteract = true;
        _monsterEmotion.AffectMonster();
        AudioManager.instance.PlayWithPitch(_audioMonsterInteraction, 1f);
        OnSoundPlayed?.Invoke();
    }

    IEnumerator PlayWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound();
    }

    IEnumerator PlayWithDelayMonster(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayMonsterSound();
    }
}
