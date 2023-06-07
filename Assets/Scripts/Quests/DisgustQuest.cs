using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisgustQuest : LevelQuest
{
    public enum QuestSteps
    {
        CollectingIngredients,
        Cutting,
        Flipping,
        Mixing,
        Serving
    }

    public static DisgustQuest Instance;
    public QuestSteps QuestStep { get; private set; }

    [field: SerializeField]
    public new QuestState State { get; private set; }

    [field:SerializeField]
    public List<IngredientName> IngredientsToPickup { get; private set; }

    [SerializeField] GameObject _recipe;
    [SerializeField] GameObject _foodPlate;

    void OnEnable()
    {
        DisgustQuestTrigger.instance.OnStartQuest += StartQuest;
        IngredientPickup.OnAllIngredientsCollected += IngredientsCollected;
        IngredientCutting.OnCuttingComplete += CuttingCompleted;
        IngredientFlipping.OnFlippingComplete += FlippingCompleted;
        IngredientMixing.OnMixingComplete += MixingCompleted;
    }

    void OnDisable()
    {
        DisgustQuestTrigger.instance.OnStartQuest -= StartQuest;
        IngredientPickup.OnAllIngredientsCollected -= IngredientsCollected;
        IngredientCutting.OnCuttingComplete -= CuttingCompleted;
        IngredientFlipping.OnFlippingComplete -= FlippingCompleted;
        IngredientMixing.OnMixingComplete -= MixingCompleted;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("More than one DisgustQuest in scene!");

        State = base.State;
    }

    void IngredientsCollected()  // TODO: Update UI Panel
    {
        QuestStep = QuestSteps.Cutting;

        Debug.Log("Move over to the cutting board!");
    }

    void CuttingCompleted()
    {
        QuestStep = QuestSteps.Flipping;

        Debug.Log("Move over to the pan - get flipping my dude!");
    }

    void FlippingCompleted()
    {
        QuestStep = QuestSteps.Mixing;

        Debug.Log("Move over to the mixing bowl!");
    }

    void MixingCompleted()
    {
        QuestStep = QuestSteps.Serving;

        _foodPlate.SetActive(true);

        Debug.Log("Grab that disgusting ass plate and give it to the monster, BOE!");
    }

    protected override void StartQuest()
    {
        if (State == QuestState.InQuest) return;

        _recipe.SetActive(true);

        State = QuestState.InQuest;
        QuestStep = QuestSteps.CollectingIngredients;
        Debug.Log("Quest started!");
    }

    protected override void CompleteQuest()
    {
        
    }
}
