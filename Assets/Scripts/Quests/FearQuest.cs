using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearQuest : LevelQuest
{
    [SerializeField] Interactable[] torches;

    int torchesOn = 0;

    void OnEnable()
    {
        FearQuestTrigger.instance.OnStartQuest += StartQuest;
    }

    void OnDisable()
    {
        FearQuestTrigger.instance.OnStartQuest -= StartQuest;
    }

    void Update()
    {
        if (!questStarted) return;

        LightUpPath();

    }

    protected override void StartQuest()
    {
        if (questStarted) return;
        questStarted = true;

        Debug.Log("Starting Quest!");
    }

    void LightUpPath()
    {

    }

    protected override void CompleteQuest()
    {
    }
}
