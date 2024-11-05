using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageChanger 
{
    private const string KEY = "Current_Language";

    public event Action<int> OnLocalizationChange;

    public int RegionIndex
    {
        get => PlayerPrefs.GetInt(KEY);
        set
        {
            PlayerPrefs.SetInt(KEY, value);
            OnLocalizationChange?.Invoke(value);
        }
    }
    public void ChangeIndexWithoutNotify(int index)
    {
        PlayerPrefs.SetInt(KEY, index);
    }
    public void ChangeIndex(int index) => RegionIndex = index;
}
