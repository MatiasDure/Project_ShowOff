using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoTracker : MonoBehaviour
{
    public static PlayerInfoTracker Instance;

    public bool GameOver { get; private set; }

    [SerializeField] List<string> questNames = new List<string>();

    int questsCompleted = 0;

    Dictionary<string, bool> quests = new Dictionary<string, bool>();

    void OnEnable()
    {
        LevelQuest.OnQuestComplete += QuestCompleted;
        GameOverHandler.OnGameRestart += GameRestart;
    }

    void OnDisable()
    {
        LevelQuest.OnQuestComplete -= QuestCompleted;
        GameOverHandler.OnGameRestart -= GameRestart;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        FillDictionary();
    }

    void QuestCompleted(string questName)
    {
        if (!quests[questName])
        {
            quests[questName] = true;
            questsCompleted++;
        }


        if (questsCompleted == 4)
        {
            GameOver = true;
        }
    }

    void FillDictionary()
    {
        for(int i = 0; i < questNames.Count; i++)
        {
            quests.Add(questNames[i], false);
        }
    }

    void GameRestart()
    {
        questsCompleted = 0;

        questNames.Clear();
        FillDictionary();
    }
}
