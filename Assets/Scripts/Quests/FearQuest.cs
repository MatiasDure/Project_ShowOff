using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FearQuest : LevelQuest
{
    public static event Action<Vector2> OnNextMonsterWaypoint;
    public static event Action OnFearQuestStart;
    public static event Action OnFearQuestEndTranstiion;

    [SerializeField] List<GameObject> torchesGO;
    [SerializeField] float firstTorchEnableSec;

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

        base.StartQuest();
        SetupTorches();
        State = QuestState.InQuest;
        StartCamTransition();
        Debug.Log("Quest started!");
    }

    protected override void CompleteQuest()
    {
        base.CompleteQuest();
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

    void StartCamTransition()
    {
        OnFearQuestStart?.Invoke();
        GameState.Instance.IsFrozen = true;
        StartCoroutine(LightFirstTorch(firstTorchEnableSec));
    }

    IEnumerator LightFirstTorch(float delay)
    {
        yield return new WaitForSeconds(delay);

        EnableTorch(torches[0], new Vector2(torches[0].transform.position.x + torches[0].MonsterPosition.x, torches[0].transform.position.z + torches[0].MonsterPosition.y));
        OnFearQuestEndTranstiion?.Invoke();
        GameState.Instance.IsFrozen = false;
    }
}
