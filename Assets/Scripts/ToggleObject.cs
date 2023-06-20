using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : InteractableReaction
{
    [SerializeField] GameObject _toggleObj;
    [SerializeField] private bool _disableOnExit = true;
    //[SerializeField] private string _soundToPlay = null;
    [SerializeField] private string _soundToPlayOn = null;
    [SerializeField] private string _soundToPlayOff = null;
    [SerializeField] private string _monsterSound = null;

    private bool _isOn = false;


    protected override void Awake()
    {
        base.Awake();
        
        if(_toggleObj == null)
        {
            Debug.LogWarning("No GameObject passed to toggle!");
            Destroy(this);
        }
        
        if(_disableOnExit) InteractableScript.OnInteractableDeactivated += DeactivateObj;
    }
    
    private void ToggleObj()
    {
        //orestis
        //_toggleObj.SetActive(!_toggleObj.activeInHierarchy);

        _toggleObj.SetActive(!_toggleObj.activeInHierarchy);

        bool isActive = _toggleObj.activeInHierarchy;
                    
        if (isActive && _soundToPlayOn != null)
        {
            AudioManager.instance.PlayWithPitch(_soundToPlayOn, 1f);
        }
        else if (!isActive && _soundToPlayOff != null)
        {
            AudioManager.instance.PlayWithPitch(_soundToPlayOff, 1f);
            StartCoroutine(PlayNextSound());
        }

        //if (_soundToPlay == null) return;

        //AudioManager.instance.PlayWithPitch(_soundToPlay,1f);
    }

    private void DeactivateObj() => _toggleObj.SetActive(false);

    protected override void Interact(InteractionInformation info) => ToggleObj();

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        if(_disableOnExit) InteractableScript.OnInteractableDeactivated -= DeactivateObj;
    }
    IEnumerator PlayNextSound()
    {
        yield return new WaitForSeconds(AudioManager.instance.GetClipLength(_soundToPlayOff));

        AudioManager.instance.PlayWithPitch(_monsterSound, 1);
    }
}
