using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelQuest : MonoBehaviour
{
    protected enum QuestState { WAITING, IN_QUEST }

    protected QuestState state = QuestState.WAITING;
    protected abstract void StartQuest();

    protected abstract void CompleteQuest();
}
