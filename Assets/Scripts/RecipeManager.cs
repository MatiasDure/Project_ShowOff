using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] List<string> _stepNames;
    [SerializeField] List<GameObject> _stepGO;


    Dictionary<string, GameObject> _recipeSteps = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        IngredientPickup.OnIngredientCollected += GetIngredientName;
    }

    void OnDisable()
    {
        IngredientPickup.OnIngredientCollected -= GetIngredientName;
    }

    void Awake()
    {
        FillDictionary();
    }

    void RecipeStepComplete(string _pStep)
    {
        _recipeSteps[_pStep].SetActive(true);
    }

    void RecipeComplete()
    {

    }
    
    void GetIngredientName(GameObject _pStep)
    {
        IngredientName _ingrName = _pStep.GetComponent<Ingredient>().IngrName;
        string _nameString = _ingrName.ToString();

        RecipeStepComplete(_nameString);
    }

    /* 
     * FillDictionary automatically fills in the _recipeSteps dictionary based on the values
     * in _stepNames and _stepGO. Fill in those 2 lists in the inspector. The items in both lists
     * should match (_stepNames[0] = _stepGO[0], etc...)
     */
    void FillDictionary()
    {
        if (_stepNames == null || _stepGO == null) return;

        if (_stepNames.Count != _stepGO.Count)
        {
            throw new Exception("StepNames list amount and StepGO list amount in RecipeManager, do not match!");
        }

        for(int i = 0; i < _stepNames.Count; i++)
        {
            _recipeSteps.Add(_stepNames[i], _stepGO[i]);
        }
    }
}
