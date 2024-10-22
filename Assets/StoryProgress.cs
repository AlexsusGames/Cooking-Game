using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryProgress : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private Tutorial tutor;
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

        CurrentDay = currentDay;
    }

    public void Save() => PlayerPrefs.SetInt(SAVE_KEY, currentDay + 1);
}
