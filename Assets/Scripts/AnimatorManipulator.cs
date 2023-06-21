using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorManipulator : MonoBehaviour
{
    public enum Params
    {
        IsMoving,
        IsInteracting
    };

    [SerializeField] private string _moveParam;

    Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Interactable.OnInteracted += Interacted;
    }

    // Update is called once per frame
    void Update()
    {
        var state = _animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Interaction") && state.normalizedTime >= 1) SetParamValue(Params.IsInteracting, false);
    }

    private void Interacted() => SetParamValue(Params.IsInteracting, true);

    public void SetParamValue(Params pParam, bool pValue)
    {
        _animator.SetBool(pParam.ToString(), pValue);
    }

    private void OnDestroy()
    {
        Interactable.OnInteracted -= Interacted;
    }
}
