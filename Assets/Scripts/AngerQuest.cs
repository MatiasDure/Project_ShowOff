using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable), typeof(TrafficLight))]
public class AngerQuest : MonoBehaviour
{
    [Tooltip("The Positions to move the player to after illegal moves")]
    [SerializeField] private Transform[] _playerPositions;

    [SerializeField] private PopUp _monsterPopUp;

    private PlayerMoveBehaviour _playingPlayerMove;
    private CharacterController _characterController;
    
    private Interactable _interactable;
    private TrafficLight _trafficLight;

    private TrafficLight.State _state;
    private bool _isPlaying;
    private bool _gameWon;
    private int _interactedCount = 0;

    public bool GameWon => _gameWon;
    public bool IsPlaying => _isPlaying;
    public int InteractedCount => _interactedCount;

    public static AngerQuest Instance { get; private set; }

    public static event Action OnIllegalMove;
    public static event Action OnQuestFinished;
    public static event Action OnTouchedMonster;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this.gameObject);

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

            if (_interactedCount != 3)
            {
                OnTouchedMonster?.Invoke();
                return;
            }

            SetGameWon();

            return;
        }

        AssignPlayer(info);
        StartGame();
    }

    IEnumerator WinPopUp()
    {
        DisplayPopUp("You Won!");

        yield return new WaitForSeconds(4);

        _monsterPopUp._container.SetActive(false);
    }

    private void SetGameWon()
    {
        _trafficLight.StopLights();
        OnQuestFinished?.Invoke();
        _gameWon = true;

        StartCoroutine(WinPopUp());
    }

    private void DisplayPopUp(string pText = "", Image pImage = null)
    {
        _monsterPopUp._container.SetActive(true);
        _monsterPopUp._text.text = pText;
        _monsterPopUp._image = pImage;
    }

    private void StartGame()
    {
        _isPlaying = true;
        _trafficLight.StartLights();
        OnTouchedMonster?.Invoke();

        GameState.Instance.IsFrozen = true;
    }

    private void AssignPlayer(InteractionInformation info)
    {
        _playingPlayerMove = info.ObjInteracted.GetComponent<PlayerMoveBehaviour>();
        _characterController = info.ObjInteracted.GetComponent<CharacterController>();
    }

    private void CheckState()
    {
        if (GameState.Instance.IsFrozen || 
            _playingPlayerMove == null || 
            LegalMove() ||
            !_playingPlayerMove.IsMoving ||
            _gameWon) return;

        _interactable.ForceExit();
        //OnIllegalMove?.Invoke();
        _characterController.enabled = false;
        _playingPlayerMove.transform.position = _playerPositions[_interactedCount].position;
        _characterController.enabled = true;
    }

    private bool LegalMove() => _state == TrafficLight.State.Go || _state == TrafficLight.State.Warning || _state == TrafficLight.State.None;
}

[Serializable]
public class PopUp
{
    public GameObject _container;
    public Image _image;
    public TextMeshProUGUI _text;
}
