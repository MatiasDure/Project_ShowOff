using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelQuest : MonoBehaviour
{
    protected bool questStarted = false;
    protected abstract void StartQuest();

    protected abstract void CompleteQuest();
}
