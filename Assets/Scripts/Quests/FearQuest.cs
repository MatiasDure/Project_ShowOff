using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearQuest : LevelQuest
{
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
        if (state == QuestState.InQuest) return;

        SetupTorches();
        state = QuestState.InQuest;
        Debug.Log("Quest started!");
    }

    protected override void CompleteQuest()
    {
        state = QuestState.Waiting;
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

    void EnableTorch(PathTorch pPathTorch)
    {
        if (state == QuestState.Waiting) return;

        if (pPathTorch == torches[torchesOn])
        {
            pPathTorch.EnableTorch();
            torchesOn++;
        }

        if(torchesOn == torches.Count)
        {
            CompleteQuest();
        }
    }

    // TODO: Do we need to disable torches? Maybe disable all when a wrong torch is intaracted with.
    void DisableTorch(PathTorch pPathTorch)
    {
        if (state == QuestState.Waiting) return;
    }

    void TorchSubscribe(PathTorch pPathTorch)
    {
        pPathTorch.OnTorchEnable += EnableTorch;
        pPathTorch.OnTorchDisable += DisableTorch;
    }

    void TorchUnsubscribe()
    {
        foreach(PathTorch pathTorch in torches)
        {
            pathTorch.OnTorchEnable -= EnableTorch;
            pathTorch.OnTorchDisable -= DisableTorch;
        }
    }
}
