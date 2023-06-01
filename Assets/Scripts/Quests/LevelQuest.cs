using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelQuest : MonoBehaviour
{
    public enum QuestState { Waiting, InQuest }

    protected QuestState state = QuestState.Waiting;
    protected abstract void StartQuest();

    protected abstract void CompleteQuest();
}
