using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoryProgress : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private Tutorial tutor;
    [SerializeField] private StoryEndView storyEndView;
    [SerializeField] private StoryEndConfig avarageStoryConfig;
    [SerializeField] private GameObject hospitalButton;
    [SerializeField] private Bank bank;
    [SerializeField] private QuestData questConfig;
    [Inject] private QuestHandler questHandler;
    private const string SAVE_KEY = "story_progress";

    private int currentDay;

    public static int CurrentDay;

    public void Load()
    {
        
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            currentDay = PlayerPrefs.GetInt(SAVE_KEY);
        }
        else currentDay = 1;

        if(currentDay == 1)
        {
            tutor.StartTutor();
        }

        if(currentDay > 11 && currentDay < 21)
        {
            hospitalButton.SetActive(true);
        }

        if(currentDay == 12)
        {
            questHandler.AddQuest(questConfig);
        }

        if(currentDay == 21)
        {
            questHandler.CompleteQuest(questConfig);
            storyEndView.ShowEndStory(avarageStoryConfig);
        }

        if(currentDay == 13)
        {
            bank.Change(1000);
        }

        CurrentDay = currentDay;
    }

    public void Save() => PlayerPrefs.SetInt(SAVE_KEY, currentDay + 1);
}
