using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SadnessQuestTrigger))]
public class SadnessQuest : LevelQuest
{
    [SerializeField] private ActivateObjects _balloonActivator;
    [SerializeField] private Ammo _ammo;
    [SerializeField] private PopUp _monsterPopUp;

    private SadnessQuestTrigger _trigger;

    private void Awake()
    {
        _trigger = GetComponent<SadnessQuestTrigger>();

        state = QuestState.Waiting;
        _trigger.OnStartQuest += StartQuest;

        if (_ammo == null) _ammo = FindObjectOfType<Ammo>();
    }


    private void Update()
    {
        if(state == QuestState.InQuest && 
            !_ammo.AmmoAvailable  )
        {
            CompleteQuest();
        }
    }

    protected override void CompleteQuest()
    {
        _balloonActivator.DeactivateAllObjects();
        HoldToggleCamera.Instance.Toggle();
        state = QuestState.Waiting;

        StartCoroutine(DisplayWin());
    }

    private IEnumerator DisplayWin()
    {
        _monsterPopUp._container.SetActive(true);
        _monsterPopUp._text.text = "Game Finished! Your Score: " + ScoreSystem.Instance.Score;
        
        yield return new WaitForSeconds(6f);

        _monsterPopUp._container.SetActive(false);
    }

    protected override void StartQuest()
    {
        if (state == QuestState.InQuest) return;

        _balloonActivator.ActivateAllObjects();
        state = QuestState.InQuest;
    }

    private void OnDestroy()
    {
        _trigger.OnStartQuest -= StartQuest;
    }
}
