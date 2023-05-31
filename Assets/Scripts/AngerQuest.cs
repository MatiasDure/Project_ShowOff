using System;
using UnityEngine;

[RequireComponent(typeof(Interactable), typeof(TrafficLight))]
public class AngerQuest : MonoBehaviour
{
    private PlayerMoveBehaviour _playingPlayerMove;
    
    private Interactable _interactable;
    private TrafficLight _trafficLight;

    private TrafficLight.State _state;
    private bool _isPlaying;
    private bool _gameWon;
    private int _interactedCount = 0;

    public bool GameWon => _gameWon;

    public static event Action OnIllegalMove;
    public static event Action OnQuestFinished;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _trafficLight = GetComponent<TrafficLight>();

        _interactable.OnInteractableActivated += CheckInteraction;
        _trafficLight.OnTrafficStateChanged += (TrafficLight.State pLightState) => _state = pLightState;

        _gameWon = false;
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckInteraction(InteractionInformation info)
    {
        if (_isPlaying)
        {
            _interactedCount++;

            if (_interactedCount != 3) return;

            //game won
            SetGameWon();
        }

        if (_playingPlayerMove != null) return;

        AssignPlayer(info);
        StartGame();
    }

    private void SetGameWon()
    {
        _trafficLight.StopLights();
        OnQuestFinished?.Invoke();
        _gameWon = true;
    }

    private void StartGame()
    {
        _isPlaying = true;
        _trafficLight.StartLights();
    }

    private void AssignPlayer(InteractionInformation info) => _playingPlayerMove = info.ObjInteracted.GetComponent<PlayerMoveBehaviour>();

    private void CheckState()
    {
        if (GameState.Instance.IsFrozen || 
            _playingPlayerMove == null || 
            LegalMove() ||
            !_playingPlayerMove.IsMoving ||
            _gameWon) return;

        OnIllegalMove?.Invoke();
    }

    private bool LegalMove() => _state == TrafficLight.State.Go || _state == TrafficLight.State.Warning || _state == TrafficLight.State.None;
}
