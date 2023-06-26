using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class LevelQuest : MonoBehaviour
{
    public enum QuestState { Waiting, InQuest }

    protected QuestState State = QuestState.Waiting;

    public static event Action<string> OnQuestComplete;

    //[SerializeField] protected AudioClip StartQuestSound;
    //[SerializeField] protected AudioClip EndQuestSound;
    [SerializeField] private string StartQuestSound;
    [SerializeField] private string EndQuestSound;

    protected virtual void StartQuest()
    {
        if (StartQuestSound == null) Debug.Log("No StartQuestSound found.");
        else
        {
            AudioManager.instance.PlayWithPitch(StartQuestSound, 1f);
        }
    }

    protected virtual void CompleteQuest(string questName)
    {
        OnQuestComplete?.Invoke(questName);

        if (EndQuestSound == null) Debug.Log("No EndQuestSound found.");
        else
        {
            AudioManager.instance.PlayWithPitch(EndQuestSound, 1f);
        }
    }
}
