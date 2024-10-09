using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestTabView : MonoBehaviour
{
    [SerializeField] private RectTransform questViewPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private Dictionary<QuestData, QuestView> viewMap = new();

    [Inject]
    public void Construct(QuestHandler dataHandler)
    {
        dataHandler.OnQuestAdded += CreateQuest;
        dataHandler.OnQuestProgressChange += ChangeQuestProgress;
        dataHandler.OnQuestCompleted += CompleteQuest;
    }

    private void CreateQuest(QuestData data)
    {
        var parent = GetFreeParent();

        var obj = Instantiate(questViewPrefab, parent);
        obj.TryGetComponent(out QuestView view);
        view.UpdateView(data);

        viewMap[data] = view;
    }

    private void ChangeQuestProgress(QuestData data) => viewMap[data].UpdateView(data);

    private void CompleteQuest(QuestData data)
    {
        Action callBack = Rebuild;

        viewMap[data].Complete(callBack);
        viewMap.Remove(data);
    }

    private Transform GetFreeParent()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].childCount == 0)
            {
                return spawnPoints[i];
            }
        }

       throw new System.Exception("There is no more point for quest");
    }

    private void Rebuild()
    {
        if(viewMap.Count > 0)
        {
            List<RectTransform> rectTransform = new();

            foreach (var item in viewMap.Keys)
            {
                var view = viewMap[item];
                view.TryGetComponent(out RectTransform transform);
                transform.SetParent(null);
                rectTransform.Add(transform);
            }

            for (int i = 0;i < rectTransform.Count; i++)
            {
                var newPosition = GetFreeParent();
                rectTransform[i].SetParent(newPosition);
                rectTransform[i].localPosition = Vector3.zero;
            }
        }
    }
}
