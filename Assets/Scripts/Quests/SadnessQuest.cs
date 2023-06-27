using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SadnessQuestTrigger))]
public class SadnessQuest : LevelQuest
{
    [SerializeField] private ActivateObjects _balloonActivator;
    [SerializeField] private Ammo _ammo;
    [SerializeField] private PopUp _monsterPopUp; //should go into the base class
    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private float _scoreShowSeconds = 2f;

    private SadnessQuestTrigger _trigger;

    //should go into the base class
    public static SadnessQuest Instance { get; private set; }
    public QuestState CurrentQuestState => State;

    private bool _displayingScore = false;
    private int _bulletsDestroyed = 0;

    private void Awake()
    {
        //Creating instance --- Should be in base awake function
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        _trigger = GetComponent<SadnessQuestTrigger>();

        State = QuestState.Waiting;
        _trigger.OnStartQuest += StartQuest;

        if (_ammo == null) _ammo = FindObjectOfType<Ammo>();
    }

    private void Start()
    {
        Bullet.OnBulletDestroyed += BulletDestroyed;
        ResetQuest();
    }

    private void Update()
    {
        if(State == QuestState.InQuest &&
            _ammo.StartingAmmoCount == _bulletsDestroyed)
        {
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
        base.CompleteQuest("Sadness");

        _balloonActivator.DeactivateAllObjects();
        State = QuestState.Waiting;
        ToggleCamera.Instance.SwitchCamera("PlayerCam");
        _ammo.EnableInfiniteAmmo();

        StartCoroutine(DisplayWin());
    }

    private IEnumerator DisplayWin()
    {
        _monsterPopUp._container.SetActive(true);
        _monsterPopUp._text.text = "Game Finished! Your Score: " + ScoreSystem.Instance.Score;
        _displayingScore = true;

        yield return new WaitForSeconds(6f);

        _monsterPopUp._container.SetActive(false);
        ResetQuest();
        _displayingScore = false;
    }

    protected override void StartQuest()
    {
        if (State == QuestState.InQuest ||
            _displayingScore) return;
        base.StartQuest();

        
        _scoreBoard.SetActive(true);
        StartCoroutine(ShowScores());
        _balloonActivator.ActivateAllObjects();
        State = QuestState.InQuest;
        _ammo.DisableInfiniteAmmo();
    }

    private IEnumerator ShowScores()
    {
        GameState.Instance.IsFrozen = true;
        Debug.Log(ToggleCamera.Instance);
        ToggleCamera.Instance.SwitchCamera("ScoreCam");

        yield return new WaitForSeconds(_scoreShowSeconds);
        
        ToggleCamera.Instance.SwitchCamera("PlayerCam");
        GameState.Instance.IsFrozen = false;
    }

    private void ResetQuest()
    {
        _bulletsDestroyed = 0;
        _monsterPopUp._text.text = "Try to shoot as many balloons as posible";
        ScoreSystem.Instance.ResetScore();
        _scoreBoard.SetActive(false);
    }

    private void BulletDestroyed()
    {
        if (CurrentQuestState != QuestState.InQuest) return;

        _bulletsDestroyed++;
    }

    private void OnDestroy()
    {
        Bullet.OnBulletDestroyed -= BulletDestroyed;
        _trigger.OnStartQuest -= StartQuest;
    }
}
