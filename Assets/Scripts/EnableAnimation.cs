using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnableAnimation : InteractableReaction
{
    [SerializeField] private string _propertyName = null;

    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();   
    }

    protected override void Interact(InteractionInformation obj)
    {
        Animate();
    }

    private void Animate()
    {
        if (_propertyName == null) return;

        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        _animator.SetBool(_propertyName, true);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        _animator.SetBool(_propertyName, false);
    }
}
