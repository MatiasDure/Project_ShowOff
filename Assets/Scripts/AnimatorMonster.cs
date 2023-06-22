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

    // Update is called once per frame
    void Update()
    {
        var state = _animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("REACTION") && state.normalizedTime >= 1)
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
