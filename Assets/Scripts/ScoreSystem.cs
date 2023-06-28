using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }
    public int Score { get; private set; }

    public Action<int> OnScoreModified;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ModifyScore(int pModifyBy = 1)
    {
        Score += pModifyBy;
        OnScoreModified?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreModified?.Invoke(Score);
    }
}
