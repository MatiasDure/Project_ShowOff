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

    public List<string> Finished { 
        get 
        {
            List<string> fin = new List<string>();

            foreach(var quest in quests)
            {
                if (!quest.Value) continue;

                fin.Add(quest.Key);
            }

            return fin;        
        } 
    } 

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameOver = true;
        }
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


        if (questsCompleted == quests.Count)
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
        GameOver = false;

        questsCompleted = 0;

        quests.Clear();
        FillDictionary();
    }
}
