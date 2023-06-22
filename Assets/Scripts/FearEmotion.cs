using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class FearEmotion : MonsterEmotion
{

    [SerializeField] private CinemachineFreeLook _camera;
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private float _shakeTime = .3f;
    [SerializeField] private float _intensity = .2f;

    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
    private Vignette _vignette;

    // Start is called before the first frame update

    void Start()
    {
        if (_globalVolume.profile.TryGet<Vignette>(out _vignette)) { }
    }

    public override void AffectMonster()
    {
        AudioManager.instance.PlayWithPitch(_emotionSound, 1f);

        if (_camera == null) return;

        if (_multiChannelPerlin == null) _multiChannelPerlin = _camera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        _multiChannelPerlin.m_AmplitudeGain = 1;
        ShowEmotion();
        _vignette.intensity.value = 1f;

        yield return new WaitForSeconds(.5f);

        HideEmotion();
        _multiChannelPerlin.m_AmplitudeGain = 0;

    }

    private void Update()
    {
        if (_vignette.intensity.value > 0) _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0, .01f);
    }
}
