using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<QuestData> quests;

    [SerializeField] private UnityEvent OnTutorStart;
    [SerializeField] private UnityEvent OnTutorFinish;

    [Inject] private QuestHandler questHandler;
    public void StartTutor()
    {
        OnTutorStart?.Invoke();
        questHandler.OnQuestCompleted += ActivateNextQuest;
        questHandler.AddQuest(quests[0]);
    }
    private void FinishTutor()
    {
        OnTutorFinish?.Invoke();
        questHandler.OnQuestCompleted -= ActivateNextQuest;
    }

    private void ActivateNextQuest(QuestData quest)
    {
        quests.Remove(quest);

        questHandler.AddQuest(quests[0]);

        if (quests.Count == 1)
        {
            FinishTutor();
        }
    }
}
