using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDataProvider 
{
    private SoundData soundData;
    private const string KEY = "SoundDataSave";

    public event Action<SoundData> OnDataChanged;

    private void SaveData()
    {
        string save = JsonUtility.ToJson(soundData);
        PlayerPrefs.SetString(KEY, save);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(KEY))
        {
            string save = PlayerPrefs.GetString(KEY);
            soundData = JsonUtility.FromJson<SoundData>(save);
        }
        else
        {
            soundData = new SoundData()
            {
                Music = 0.5f,
                Sound = 0.5f
            };
        }
    }

    public SoundData GetData()
    {
        LoadData();
        return soundData;
    }

    public void ChangeData(SoundData data)
    {
        soundData = data;
        OnDataChanged?.Invoke(data);

        SaveData();
    }
}

[System.Serializable]
public class SoundData
{
    public float Music;
    public float Sound;
}
