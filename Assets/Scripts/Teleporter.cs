using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractableReaction
{
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

    protected override void Interact(InteractionInformation info) => Teleport(info.ObjInteracted);

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

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
