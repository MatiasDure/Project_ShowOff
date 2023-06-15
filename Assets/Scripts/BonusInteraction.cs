using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BonusInteraction : MonoBehaviour
{
    [SerializeField] ParticleSystem _ps;
    [SerializeField] InteractableReaction _interactable;

    void OnEnable()
    {
        _interactable.OnInteractableReaction += DisableGO;
    }

    void OnDisable()
    {
        _interactable.OnInteractableReaction -= DisableGO;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ps.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ps.Stop();
        }
    }

    void DisableGO()
    {
        gameObject.SetActive(false);
    }
}
