using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class DisgustEmotion : MonsterEmotion
{
    [SerializeField] Animator _animator;

    public override void AffectMonster()
    {
        AudioManager.instance.PlayWithPitch(_emotionSound, 1f);
        _animator.SetBool("IsTriggered", true);
    }
}
