using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SadnessEmotion : MonsterEmotion, IHittable
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private float _shakeTime = .3f;
    [SerializeField] private float _intensity = .2f;
    [SerializeField] private AnimatorMonster _animator;

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
        _animator.UpdateParameter(AnimatorMonster.Params.IsTriggered, true);
        ToggleCamera.Instance.SwitchCamera("MonsterCam");

        if (_camera == null) return;

        if (_multiChannelPerlin == null) _multiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

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

    public void Hit()
    {
        AffectMonster();
    }
}
