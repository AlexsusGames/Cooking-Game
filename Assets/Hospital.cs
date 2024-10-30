using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Hospital : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private Bank bank;
    [Space]
    [Header("EndStory")]
    [SerializeField] private StoryEndView storyEndView;
    [SerializeField] private StoryEndConfig goodEndConfig;
    [Inject] private FamilyStateManager familyStateManager;
    [Inject] private QuestHandler questHandler;
    private const string QUEST_REQUEST = "desease";
    private const string KEY = "Hospital_Key";
    private const int PAYMENT_VALUE = 5000;

    private bool isPaid;

    public void Pay()
    {
        if (bank.Has(PAYMENT_VALUE))
        {
            bank.Change(-PAYMENT_VALUE);
            questHandler.TryChangeProgress(QUEST_REQUEST);
            familyStateManager.EndStory();
            isPaid = true;
        }
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey(KEY))
        {
            storyEndView.ShowEndStory(goodEndConfig);
        }
    }

    public void Save()
    {
        if(isPaid)
        {
            PlayerPrefs.SetInt(KEY, PAYMENT_VALUE);
        }
    }
}
