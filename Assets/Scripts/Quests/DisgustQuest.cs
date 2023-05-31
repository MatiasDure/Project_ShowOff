using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisgustQuest : LevelQuest
{
    [field: SerializeField]
    public List<IngredientName> IngredientsToPickup { get; private set; }

    [SerializeField] GameObject _recipe;

    public static new QuestState state { get; private set; }

    void OnEnable()
    {
        DisgustQuestTrigger.instance.OnStartQuest += StartQuest;
        IngredientPickup.OnAllIngredientsCollected += IngredientsCollected;
    }

    void OnDisable()
    {
        DisgustQuestTrigger.instance.OnStartQuest -= StartQuest;
        IngredientPickup.OnAllIngredientsCollected -= IngredientsCollected;
    }

    private void Awake()
    {
    }

    void Start()
    {
        state = base.state;
    }

    void Update()
    {
        
    }

    void IngredientsCollected()  // TODO: Update UI Panel
    {
        Debug.Log("Move over to the cutting board!");
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
