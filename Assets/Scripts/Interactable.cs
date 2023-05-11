using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] private KeyCode[] _keys;
    [SerializeField] private float _radius;
    [SerializeField] private bool _KeyDown = true;

    private SphereCollider _collider;
    private bool _clicked;
    private KeyCode _keyClicked;

    public static event Action<KeyCode[]> OnEnteredInteractionZone;
    public static event Action OnLeftInteractionZone;

    public event Action<InteractionInformation> OnInteractableActivated;
    public event Action OnInteractableDeactivated;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        SetCollider();
    }

    private void SetCollider()
    {
        _collider.isTrigger = true;
        _collider.radius = _radius;
    }

    private void Update() => GetKeyPressed();

    private void GetKeyPressed()
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            if (!AcceptableKeyPressed(i)) continue;

            _clicked = true;
            _keyClicked = _keys[i];
        }
    }

    private bool AcceptableKeyPressed(int i) 
    {
        return (_KeyDown &&
            Input.GetKeyDown(_keys[i])) ||
            (!_KeyDown &&
            Input.GetKey(_keys[i]));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;

        OnEnteredInteractionZone?.Invoke(_keys);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG) ||
            !_clicked) return;

        OnInteractableActivated?.Invoke(new InteractionInformation(_keyClicked));
        ResetInteractionInfo();
    }

    private void ResetInteractionInfo()
    {
        _clicked = false;
        _keyClicked = KeyCode.None;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;

        OnInteractableDeactivated?.Invoke();
        OnLeftInteractionZone?.Invoke();
    }

}

public class InteractionInformation
{
    public KeyCode KeyPressed { get; private set; }
    
    public InteractionInformation(KeyCode pPressed)
    {
        KeyPressed = pPressed;
    }
}
