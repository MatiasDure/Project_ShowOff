using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearTreeShake : InteractableReaction
{
    [SerializeField] ParticleSystem _ps;
    [SerializeField] string _treeShakeSound;
    protected override void Interact(InteractionInformation obj)
    {
        base.Interact(obj);
        _ps.Play();
        AudioManager.instance.PlayWithPitch(_treeShakeSound,1f);
        InteractableScript.ForceExit();
    }
}
