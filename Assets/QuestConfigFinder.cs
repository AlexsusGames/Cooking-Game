using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestConfigFinder 
{
    private QuestData[] quests;

    private Dictionary<string, QuestData> questMap = new();

    public void Init()
    {
        quests = Resources.LoadAll<QuestData>("Quests");

        for (int i = 0; i < quests.Length; i++)
        {
            questMap[quests[i].QuestId] = quests[i];
        }
    }

    public QuestData GetQuestById(string id) => questMap[id];

}
