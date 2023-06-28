using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractableReaction
{
    [SerializeField] private int _sceneToTeleport;
    
    private List<Transform> pointsToTeleport = new List<Transform>();

    private CharacterController _characterController;

    protected override void Awake()
    {
       base.Awake();
       
        for(int i = 0; i < transform.childCount; i++)
       {
            pointsToTeleport.Add(transform.GetChild(i));
       }
    }

    private int GenerateRandomIndex() =>  Random.Range(0, pointsToTeleport.Count);

    protected override void Interact(InteractionInformation info) => TeleportScene();//Teleport(info.ObjInteracted);

    private void Teleport(GameObject objInteracted)
    {
        Transform ranPoint = pointsToTeleport[GenerateRandomIndex()];
        Vector3 newPos = ranPoint.position;
        Quaternion newRot = ranPoint.rotation;

        if (_characterController == null) _characterController = objInteracted.GetComponent<CharacterController>();

        InteractableScript.ForceExit();

        _characterController.enabled = false;
        objInteracted.transform.position = newPos;
        objInteracted.transform.rotation = newRot;
        _characterController.enabled = true;
    }

    private void TeleportScene()
    {
        if (LoadingScene.Instance == null) return;

        // CHANGE SCENE NUMBERS ON FINAL BUILD TO MATCH BEDROOM! (HERE AND IN GameOver!!!)
        if (_sceneToTeleport == 0 && PlayerInfoTracker.Instance.GameOver)
        {
            _sceneToTeleport = 5;
        }
        LoadingScene.Instance.LoadScene(_sceneToTeleport);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
