using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelQuest : MonoBehaviour
{
    public enum QuestState { Waiting, InQuest }

    protected QuestState State = QuestState.Waiting;

    [SerializeField] protected AudioClip StartQuestSound;
    [SerializeField] protected AudioClip EndQuestSound;

    protected virtual void StartQuest()
    {
        if (StartQuestSound == null) Debug.Log("No StartQuestSound found.");
        else
        {

        }
    }

    protected virtual void CompleteQuest()
    {
        if (EndQuestSound == null) Debug.Log("No EndQuestSound found.");
        else
        {

        }
    }
}
