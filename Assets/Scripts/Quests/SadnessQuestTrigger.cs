using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SadnessQuestTrigger : QuestTrigger<SadnessQuestTrigger>
{
    public event Action OnStartQuest;

    protected override void Interact(InteractionInformation obj)
    {
        OnStartQuest?.Invoke();
    }

}
