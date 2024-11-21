using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageChanger 
{
    private List<TMP_FontAsset> standartFonts;
    private List<TMP_FontAsset> bookFonts;
    private const string KEY = "Current_Language";

    public event Action<int> OnLocalizationChange;

    public LanguageChanger(List<TMP_FontAsset> standartFonts, List<TMP_FontAsset> bookFonts)
    {
        this.standartFonts = standartFonts;
        this.bookFonts = bookFonts;
    }

    public TMP_FontAsset GetFont(int regionIndex, bool isBook)
    {
        if (isBook)
        {
            return bookFonts[regionIndex];
        }
        return standartFonts[regionIndex];
    }

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
