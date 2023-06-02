using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerEmotion : MonsterEmotion
{
    [SerializeField] private CinemachineFreeLook _camera;
    [SerializeField] private float _shakeTime = .3f;
    [SerializeField] private float _intensity = .2f;
    
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    // Start is called before the first frame update
    void Start()
    {
        AngerQuest.OnIllegalMove += ShowEmotion;
        MonsterNavMesh.OnReachedNewPosition += HideEmotion;
    }

    public override void AffectMonster()
    {
        AudioManager.instance.PlayWithPitch(_emotionSound, 1f);

        if (_camera == null) return;
        
        if(_multiChannelPerlin == null) _multiChannelPerlin = _camera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        _multiChannelPerlin.m_AmplitudeGain = 1;
        ShowEmotion();

        yield return new WaitForSeconds(.5f);

        HideEmotion();
        _multiChannelPerlin.m_AmplitudeGain = 0;
    }
}
