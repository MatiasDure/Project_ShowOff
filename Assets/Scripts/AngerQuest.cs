using System;
using UnityEngine;

[RequireComponent(typeof(Interactable), typeof(TrafficLight))]
public class AngerQuest : MonoBehaviour
{
    private PlayerMoveBehaviour _playingPlayerMove;
    
    private Interactable _interactable;
    private TrafficLight _trafficLight;

    private TrafficLight.State _state;

    public static event Action OnIllegalMove;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _trafficLight = GetComponent<TrafficLight>();

        _interactable.OnInteractableActivated += AssignPlayer;
        _trafficLight.OnTrafficStateChanged += (TrafficLight.State pLightState) => _state = pLightState;
    }

    private void Update()
    {
        CheckState();
    }

    private void AssignPlayer(InteractionInformation info)
    {
        if (_playingPlayerMove != null) return;

        _playingPlayerMove = info.ObjInteracted.GetComponent<PlayerMoveBehaviour>();
        _trafficLight.StartLights();
    }

    private void CheckState()
    {
        if (GameState.Instance.IsFrozen || 
            _playingPlayerMove == null || 
            LegalMove() ||
            !_playingPlayerMove.IsMoving) return;

        OnIllegalMove?.Invoke();
        Debug.LogWarning("Emotional Damage!");
    }

    private bool LegalMove() => _state == TrafficLight.State.Go || _state == TrafficLight.State.Warning;

}
