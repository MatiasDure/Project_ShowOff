using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInteractable : InteractableReaction
{
    [SerializeField] AudioClip _audioClip;

    protected override void Interact(InteractionInformation obj)
    {
        PlaySound();
    }

    void PlaySound()
    {
        if(_audioClip == null)
        {
            Debug.Log("Audio clip not found!");
            return;
        }

        // Play sound here

    }
}
