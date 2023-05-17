using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FearQuestTrigger : QuestTrigger<FearQuestTrigger>
{
    public event Action OnStartQuest;
    protected override void Awake()
    {
        base.Awake();

        if (instance == null) instance = this;
        else throw new Exception("More than one FearQuestTrigger instance in scene!");
    }

    protected override void Interact(InteractionInformation obj)
    {
        OnStartQuest?.Invoke();
    }
}
