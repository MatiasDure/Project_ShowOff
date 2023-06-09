using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FearQuest : LevelQuest
{
    public static event Action<Vector2> OnNextMonsterWaypoint; 

    [SerializeField] List<GameObject> torchesGO;

    List<PathTorch> torches = new List<PathTorch>();

    int torchesOn = 0;

    void OnEnable()
    {
        FearQuestTrigger.instance.OnStartQuest += StartQuest;
    }

    void OnDisable()
    {
        FearQuestTrigger.instance.OnStartQuest -= StartQuest;
    }

    protected override void StartQuest()
    {
        if (State == QuestState.InQuest) return;

        SetupTorches();
        State = QuestState.InQuest;
        Debug.Log("Quest started!");
    }

    protected override void CompleteQuest()
    {
        State = QuestState.Waiting;
        torchesOn = 0;
        TorchUnsubscribe();
        Debug.Log("Quest complete!");
    }

    void SetupTorches()
    {
        foreach(GameObject torch in torchesGO)
        {
            PathTorch pathTorch = torch.transform.GetComponentInChildren<PathTorch>();
            TorchSubscribe(pathTorch);
            torches.Add(pathTorch);
        }
    }

    void EnableTorch(PathTorch pPathTorch, Vector2 pMonsterPos)
    {
        if (State == QuestState.Waiting) return;

        if (pPathTorch == torches[torchesOn])
        {
            pPathTorch.EnableTorch();
            OnNextMonsterWaypoint?.Invoke(pMonsterPos);
            torchesOn++;
        }

        if(torchesOn == torches.Count)
        {
            CompleteQuest();
        }
    }

    void TorchSubscribe(PathTorch pPathTorch)
    {
        pPathTorch.OnTorchEnable += EnableTorch;
    }

    void TorchUnsubscribe()
    {
        foreach(PathTorch pathTorch in torches)
        {
            pathTorch.OnTorchEnable -= EnableTorch;
        }
    }
}
