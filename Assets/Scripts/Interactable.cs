using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] private KeyCode _key;
    [SerializeField] private float _radius;

    private SphereCollider _collider;
    private bool _clicked;
    
    public event Action OnInteractableActivated;
    public event Action OnInteractableDeactivated;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = _radius;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_key)) _clicked = true; 
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG) || !_clicked) return;

        OnInteractableActivated?.Invoke();
        _clicked = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;

        OnInteractableDeactivated?.Invoke();
    }

}
