using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ScoreSystem))]
public class ScoreUI : MonoBehaviour
{
    [SerializeField] GameObject _scoreContainer;
    [SerializeField] TextMeshProUGUI _scoreText;

    private ScoreSystem _scoreSys;

    private void Awake()
    {
        _scoreSys = GetComponent<ScoreSystem>();
    }

    private void Start()
    {
        ScoreSystem.Instance.OnScoreModified += ScoreModified;
        SadnessQuest.OnQuestStarted += EnableUI;
        SadnessQuest.OnQuestComplete += DisableUI;

        ScoreModified(0);
    }

    private void DisableUI(string obj)
    {
        _scoreContainer.SetActive(false);
    }

    private void EnableUI()
    {
        _scoreContainer.SetActive(true); 
    }

    private void ScoreModified(int pNewScore)
    {
        
        if(_scoreText != null) _scoreText.text = pNewScore.ToString();

        //do other things maybe
    }

    private void OnDestroy()
    {
        SadnessQuest.OnQuestStarted -= EnableUI;
        SadnessQuest.OnQuestComplete -= DisableUI;
        ScoreSystem.Instance.OnScoreModified -= ScoreModified;
    }
}
