using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateBranch : InteractableReaction
{
    [SerializeField] private GameObject _branchPivot;

    //nico
    private bool audioIsPlaying;

    protected override void Awake()
    {
        base.Awake();

        if (_branchPivot == null) Destroy(this);
    }

    private void Rotate(InteractionInformation info)
    {
        float angle = _branchPivot.transform.rotation.eulerAngles.x;

        angle = angle > 180 ? angle - 360 : angle;

        if(info.KeyPressed == KeyCode.F &&
            angle < 45)
        {
            _branchPivot.transform.Rotate(new(2, 0, 0));
        }
        else if(info.KeyPressed == KeyCode.G &&
            angle > -45)
        {
            _branchPivot.transform.Rotate(new(-2, 0, 0));
        }
    }
    //Matias
    //protected override void Interact(InteractionInformation info) => Rotate(info);

    //Nico
    //protected override void Interact(InteractionInformation info)
    //{
    //    Rotate(info);
    //    //FindObjectOfType<AudioManager>().Play("ShakingTree");
    //    AudioManager.instance.PlayWithPitch("ShakingTree",1);
    //}

    protected override void Interact(InteractionInformation info)
    {
        if (!audioIsPlaying)
        {
            StartCoroutine(PlayAudioAndWait(info));
        }
        else
        {
            Rotate(info);
        }
    }

    private IEnumerator PlayAudioAndWait(InteractionInformation info)
    {
        audioIsPlaying = true;
        AudioManager.instance.PlayWithPitch("ShakingTree", 1);
        // Wait for the audio to finish playing
        yield return new WaitForSeconds(AudioManager.instance.GetClipLength("ShakingTree"));

        audioIsPlaying = false;
        Debug.Log("Play monster screaming sound here mothafaka!");

        Rotate(info);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();   
    }
}
