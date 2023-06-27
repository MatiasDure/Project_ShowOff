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

    private SadnessQuestTrigger _trigger;

    //should go into the base class
    public static SadnessQuest Instance { get; private set; }
    public QuestState CurrentQuestState => State;

    private bool _displayingScore = false;

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
        ResetQuest();
    }

    private void Update()
    {
        if(State == QuestState.InQuest && 
            !_ammo.AmmoAvailable)
        {
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
        base.CompleteQuest("Sadness");

        _balloonActivator.DeactivateAllObjects();
        State = QuestState.Waiting;
        HoldToggleCamera.Instance.Toggle();
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

        _balloonActivator.ActivateAllObjects();
        State = QuestState.InQuest;
        _ammo.DisableInfiniteAmmo();
    }

    private void ResetQuest()
    {
        _monsterPopUp._text.text = "Try to shoot as many balloons as posible";
        ScoreSystem.Instance.ResetScore();
    }

    private void OnDestroy()
    {
        _trigger.OnStartQuest -= StartQuest;
    }
}
