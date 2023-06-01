using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisgustQuestTrigger : QuestTrigger<DisgustQuestTrigger>
{
    public event Action OnStartQuest;

    protected override void Awake()
    {
        base.Awake();

        if (instance == null) instance = this;
        else throw new Exception("More than one DisgustQuestTrigger instance in scene!");
    }

    protected override void Interact(InteractionInformation obj)
    {
        OnStartQuest?.Invoke();
    }
}
