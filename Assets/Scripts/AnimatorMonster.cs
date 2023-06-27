using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMonster : MonoBehaviour
{
    public enum Params
    {
        IsTriggered,
        IsMoving
    }

    [SerializeField] Animator _animator;
    [Tooltip("The amount of loops for the reaction animations")]
    [SerializeField] int _reactionLoops = 1;

    // Update is called once per frame
    void Update()
    {
        var state = _animator.GetCurrentAnimatorStateInfo(0);

        //two loops for the reaction animation
        if (state.IsName("REACTION") && state.normalizedTime >= _reactionLoops)
        {
            UpdateParameter(Params.IsTriggered, false);
            ToggleCamera.Instance.SwitchCamera("PlayerCam");
        }
    }

    public void UpdateParameter(Params pParam, bool pValue)
    {
        if (_animator == null) return;

        _animator.SetBool(pParam.ToString(), pValue);
    }

}
