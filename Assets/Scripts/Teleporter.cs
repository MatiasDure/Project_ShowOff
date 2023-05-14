using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Teleporter : MonoBehaviour, IInteractable
{
    private List<Transform> pointsToTeleport = new List<Transform>();

    private CharacterController _characterController;

    private Interactable _interactableScript;

    private void Awake()
    {
       for(int i = 0; i < transform.childCount; i++)
       {
            pointsToTeleport.Add(transform.GetChild(i));
       }
    }

    private void Start()
    {
        _interactableScript = GetComponent<Interactable>();

        _interactableScript.OnInteractableActivated += Interact;
    }

    private int GenerateRandomIndex() =>  Random.Range(0, pointsToTeleport.Count);

    public void Interact(InteractionInformation info) => Teleport(info.ObjInteracted);

    private void Teleport(GameObject objInteracted)
    {
        Transform ranPoint = pointsToTeleport[GenerateRandomIndex()];
        Vector3 newPos = ranPoint.position;
        Quaternion newRot = ranPoint.rotation;

        if (_characterController == null) _characterController = objInteracted.GetComponent<CharacterController>();

        _interactableScript.ForceExit();

        _characterController.enabled = false;
        objInteracted.transform.position = newPos;
        objInteracted.transform.rotation = newRot;
        _characterController.enabled = true;
    }

    private void OnDestroy()
    {
        _interactableScript.OnInteractableActivated -= Interact;
    }
}
