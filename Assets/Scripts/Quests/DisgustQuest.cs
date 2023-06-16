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
        DisgustQuestTrigger.instance.OnMonsterInteraction += PlayerInteract;
        IngredientPickup.OnAllIngredientsCollected += IngredientsCollected;
        IngredientCutting.OnCuttingComplete += CuttingCompleted;
        IngredientFlipping.OnFlippingComplete += FlippingCompleted;
        IngredientMixing.OnMixingComplete += MixingCompleted;
    }

    void OnDisable()
    {
        DisgustQuestTrigger.instance.OnMonsterInteraction -= PlayerInteract;
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

    void CuttingCompleted()  // TODO: Update UI Panel
    {
        QuestStep = QuestSteps.Flipping;

        Debug.Log("Move over to the pan - get flipping my dude!");
    }

    void FlippingCompleted()  // TODO: Update UI Panel
    {
        QuestStep = QuestSteps.Mixing;

        Debug.Log("Move over to the mixing bowl!");
    }

    void MixingCompleted()  // TODO: Update UI Panel
    {
        QuestStep = QuestSteps.Serving;

        _foodPlate.SetActive(true);

        Debug.Log("Grab that disgusting ass plate and give it to the monster, BOE!");
    }

    void ServePlate()
    {
        ObjectPickup _platePickup;
        if (!_foodPlate.TryGetComponent<ObjectPickup>(out _platePickup)) return;

        if (!_platePickup.PickedUp) return;

        Destroy(_foodPlate);
        CompleteQuest();
    }

    void PlayerInteract()
    {
        if (QuestStep == QuestSteps.Serving)
        {
            ServePlate();
        }

        if (State == QuestState.InQuest) return;
        StartQuest();
    }

    protected override void StartQuest()
    {
        _recipe.SetActive(true);
        AudioManager.instance.PlayWithPitch("Mhm", 1f);

        State = QuestState.InQuest;
        QuestStep = QuestSteps.CollectingIngredients;
        Debug.Log("Quest started!");
    }

    protected override void CompleteQuest() // TODO: Update UI Panel
    {
        AudioManager.instance.PlayWithPitch("Slurp", 1f);
        StartCoroutine(PlayNextSound());
        Debug.Log("Quest complete!");
        this.enabled = false;
    }
    IEnumerator PlayNextSound()
    {
        yield return new WaitForSeconds(AudioManager.instance.GetClipLength("Slurp"));

        // Play your next sound here
        AudioManager.instance.PlayWithPitch("FinalNote", 1f);
    }
}

