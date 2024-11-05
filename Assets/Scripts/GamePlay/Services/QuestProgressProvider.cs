using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestProgressProvider : MonoBehaviour, IProgressDataProvider
{
    [Inject] private QuestConfigFinder configFinder;
    [Inject] private QuestHandler questHandler;

    private const string KEY = "GlobalQuestSave";
    private GlobalQuests globalQuests;

    public void Load()
    {
        configFinder.Init();

        if (PlayerPrefs.HasKey(KEY))
        {
            string save = PlayerPrefs.GetString(KEY);
            globalQuests = JsonUtility.FromJson<GlobalQuests>(save);

            for(int i = 0; i < globalQuests.Quests.Count; i++)
            {
                var quest = globalQuests.Quests[i];
                var data = configFinder.GetQuestById(quest.QuestId);

                questHandler.AddQuest(data, quest.Progress);
            }
        }
    }

    public void Save()
    {
        var questToSave = questHandler.GetCurrentQuests();

        if (questToSave.Count != 0)
        {
            globalQuests = new();

            for (int i = 0; i < questToSave.Count; i++)
            {
                GlobalQuest quest = new()
                {
                    QuestId = questToSave[i].id,
                    Progress = questToSave[i].progress
                };

                globalQuests.Quests.Add(quest);
            }
        }
        else globalQuests = new();

        string save = JsonUtility.ToJson(globalQuests);
        PlayerPrefs.SetString(KEY, save);
        PlayerPrefs.Save();
    }
}
[System.Serializable]
public class GlobalQuests
{
    public List<GlobalQuest> Quests = new();
}
[System.Serializable]
public class GlobalQuest
{
    public string QuestId;
    public int Progress;
}
