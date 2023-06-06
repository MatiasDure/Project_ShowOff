using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ScoreSystem))]
public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    private ScoreSystem _scoreSys;

    private void Awake()
    {
        _scoreSys = GetComponent<ScoreSystem>();

    }

    private void Start()
    {
        ScoreSystem.Instance.OnScoreModified += ScoreModified;
    }

    private void ScoreModified(int pNewScore)
    {
        
        if(_scoreText != null) _scoreText.text = "Your score: " + pNewScore;

        //do other things maybe
    }

    private void OnDestroy()
    {
        ScoreSystem.Instance.OnScoreModified -= ScoreModified;
    }
}
