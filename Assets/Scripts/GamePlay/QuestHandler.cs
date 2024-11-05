using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestHandler 
{
    private List<QuestData> quests = new();

    public event Action<QuestData> OnQuestAdded;
    public event Action<QuestData> OnQuestCompleted;
    public event Action<QuestData> OnQuestProgressChange;

    [Inject] private QuestConfigFinder configFinder;

    public void AddQuest(QuestData data, int progress = 0)
    {
        if (!quests.Contains(data))
        {
            quests.Add(data);
            data.CurrentProgress = progress;
            OnQuestAdded?.Invoke(data);
        }
    }

    public void CompleteQuest(QuestData data)
    {
        if(quests.Contains(data))
        {
            OnQuestCompleted?.Invoke(data);
            quests.Remove(data);
        }
    }

    public void ChangeProgress(QuestData data, int newProgress)
    {
        data.CurrentProgress += newProgress;

        OnQuestProgressChange?.Invoke(data);

        if(data.CurrentProgress >= data.Progress)
        {
            CompleteQuest(data);
        }
    }

    public List<(string id, int progress)> GetCurrentQuests()
    {
        List<(string, int)> result = new();

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].IsGlobal)
            {
                result.Add((quests[i].QuestId, quests[i].CurrentProgress));
            }
        }

        return result;
    }

    public void TryChangeProgress(string questId, int amount = 1)
    {
        var quest = configFinder.GetQuestById(questId);

        if (quest == null)
            throw new Exception($"There is no such a quest: {questId}");

        if (quests.Contains(quest))
        {
            ChangeProgress(quest, amount);
        }
    }
}
