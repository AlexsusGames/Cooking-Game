using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryProgress : MonoBehaviour, IProgressDataProvider
{
    private const string SAVE_KEY = "story_progress";

    private int currentDay;

    public void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            currentDay = PlayerPrefs.GetInt(SAVE_KEY);
        }
        else currentDay = 1;
    }

    public void Save() => PlayerPrefs.SetInt(SAVE_KEY, currentDay + 1);
}
