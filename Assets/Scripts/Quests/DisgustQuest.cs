using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisgustQuest : LevelQuest
{
    [SerializeField] GameObject _recipe;

    public static new QuestState state { get; private set; }

    void OnEnable()
    {
        DisgustQuestTrigger.instance.OnStartQuest += StartQuest;
    }

    void OnDisable()
    {
        DisgustQuestTrigger.instance.OnStartQuest -= StartQuest;
    }

    void Start()
    {
        state = base.state;
    }

    void Update()
    {
        
    }

    protected override void StartQuest()
    {
        if (state == QuestState.InQuest) return;

        _recipe.SetActive(true);

        state = QuestState.InQuest;
        Debug.Log("Quest started!");
    }

    protected override void CompleteQuest()
    {
        
    }
}
