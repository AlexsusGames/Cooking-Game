using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler 
{
    private List<QuestData> quests = new();

    public event Action<QuestData> OnQuestAdded;
    public event Action<QuestData> OnQuestCompleted;
    public event Action<QuestData> OnQuestProgressChange;

    public void AddQuest(QuestData data, int progress = 0)
    {
        if (!quests.Contains(data))
        {
            quests.Add(data);
            OnQuestAdded?.Invoke(data);
            data.CurrentProgress = progress;
        }
    }

    private void CompleteQuest(QuestData data)
    {
        OnQuestCompleted?.Invoke(data);
        quests.Remove(data);
    }

    public void ChangeProgress(QuestData data, int newProgress)
    {
        data.CurrentProgress = newProgress;

        OnQuestProgressChange?.Invoke(data);

        if(newProgress >= data.Progress)
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
                result.Add((quests[i].QuestName, quests[i].CurrentProgress));
            }
        }

        return result;
    }
}
