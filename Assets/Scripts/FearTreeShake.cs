using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearTreeShake : InteractableReaction
{
    [SerializeField] ParticleSystem _ps;
    protected override void Interact(InteractionInformation obj)
    {
        base.Interact(obj);
        _ps.Play();
        InteractableScript.ForceExit();
    }
}
