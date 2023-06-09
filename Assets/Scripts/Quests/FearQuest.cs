using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FearQuest : LevelQuest
{
    public static event Action<Vector2> OnNextMonsterWaypoint;
    public static event Action<Transform> OnTorchLit;
    public static event Action OnFearQuestStart;
    public static event Action OnFearQuestEndTranstiion;

    [SerializeField] List<GameObject> torchesGO;
    [SerializeField] float firstTorchEnableSec;
    [SerializeField] GameObject hints;

    List<PathTorch> torches = new List<PathTorch>();
    List<Interactable> interactableScripts = new List<Interactable>();

    int torchesOn = 0;
    bool questedComplete = false;

    private void Start()
    {
        foreach (GameObject torch in torchesGO)
        {
            PathTorch pathTorch = torch.transform.GetComponentInChildren<PathTorch>();
            Interactable currentInteractableScript = pathTorch.GetComponent<Interactable>();
            currentInteractableScript.enabled = false;
            interactableScripts.Add(currentInteractableScript);
            //TorchSubscribe(pathTorch);
            torches.Add(pathTorch);
        }
    }

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
        if (State == QuestState.InQuest || questedComplete) return;

        base.StartQuest();
        SetupTorches();
        hints.SetActive(true);
        State = QuestState.InQuest;
        StartCamTransition();
    }

    void CompleteQuest()
    {
        questedComplete = true;
        base.CompleteQuest("Fear");
        hints.SetActive(false);
        State = QuestState.Waiting;
        torchesOn = 0;
        TorchUnsubscribe();
    }

    void SetupTorches()
    {
        foreach(PathTorch torch in torches)
        {
            //PathTorch pathTorch = torch.transform.GetComponentInChildren<PathTorch>();
            //torch.GetComponentInChildren<Interactable>().enabled = true;
            TorchSubscribe(torch);
            //torches.Add(pathTorch);
        }

        foreach(var interactable in interactableScripts)
        {
            interactable.enabled = true;
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

            if (torchesOn == torches.Count)
            {
                CompleteQuest();
                return;
            }

            OnTorchLit?.Invoke(torches[torchesOn].transform); // For hint trail
        }
        else
        {
        AudioManager.instance.PlayWithPitch("WrongChoice", 1);
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
