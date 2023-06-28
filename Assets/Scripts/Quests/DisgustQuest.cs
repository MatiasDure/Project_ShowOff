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

    public static event Action<string> OnStepComplete;
    public static event Action<Transform> OnShowNextHint;

    public static DisgustQuest Instance;
    public QuestSteps QuestStep { get; private set; }

    [field: SerializeField]
    public new QuestState State { get; private set; }

    [field:SerializeField]
    public List<IngredientName> IngredientsToPickup { get; private set; }

    [SerializeField] GameObject _recipe;
    [SerializeField] GameObject _foodPlate;
    [SerializeField] GameObject _poopFoodPlate;
    [SerializeField] Transform _cuttingHint;
    [SerializeField] Transform _flippingHint;
    [SerializeField] Transform _mixingHint;
    [SerializeField] Transform _plateHint;
    [SerializeField] Transform _plateEatingPosition;
    [SerializeField] HintTrail _hintTrail;
    [SerializeField] Animator _animator;

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

    void IngredientsCollected()
    {
        QuestStep = QuestSteps.Cutting;
        _hintTrail.gameObject.SetActive(true);
        OnShowNextHint?.Invoke(_cuttingHint);
    }

    void CuttingCompleted()
    {
        QuestStep = QuestSteps.Flipping;

        OnStepComplete?.Invoke("Cut");
        OnShowNextHint?.Invoke(_flippingHint);
    }

    void FlippingCompleted()
    {
        QuestStep = QuestSteps.Mixing;

        OnStepComplete?.Invoke("Flip");
        OnShowNextHint?.Invoke(_mixingHint);
    }

    void MixingCompleted()
    {
        QuestStep = QuestSteps.Serving;

        _foodPlate.SetActive(true);

        OnStepComplete?.Invoke("Mix");
        OnShowNextHint?.Invoke(_plateHint);
    }

    void ServePlate()
    {
        ObjectPickup _platePickup;
        if (!_foodPlate.TryGetComponent<ObjectPickup>(out _platePickup)) return;

        if (!_platePickup.PickedUp) return;

        _hintTrail.RemoveTrail(_plateHint);
        _hintTrail.gameObject.SetActive(false);
        _foodPlate.transform.parent.SetParent(_plateEatingPosition);
        _foodPlate.transform.parent.localPosition = Vector3.zero;

        Destroy(_poopFoodPlate);
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
        base.StartQuest();

        _recipe.SetActive(true);
        AudioManager.instance.PlayWithPitch("Mhm", 1f);

        State = QuestState.InQuest;
        QuestStep = QuestSteps.CollectingIngredients;
    }

    void CompleteQuest()
    {
        base.CompleteQuest("Disgust");
        _recipe.SetActive(false);
        _animator.SetBool("IsEating", true);

        AudioManager.instance.PlayWithPitch("Slurp", 1f);
        StartCoroutine(PlayNextSound());
        this.enabled = false;
    }
    IEnumerator PlayNextSound()
    {
        yield return new WaitForSeconds(AudioManager.instance.GetClipLength("Slurp"));

        // Play your next sound here
        AudioManager.instance.PlayWithPitch("FinalNote", 1f);
    }
}

