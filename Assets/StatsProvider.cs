using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsProvider 
{
    private const string Key = "Income_Stats_Key";
    private IncomeStats data;

    private void SaveData()
    {
        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(Key, save);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(Key))
        {
            string save = PlayerPrefs.GetString(Key);
            data = JsonUtility.FromJson<IncomeStats>(save);
        }
        else
        {
            data = new();
            for (int i = 0; i < 7; i++) data.LastWeek.Add(new DayStatsData());
        }
    }

    public void AddDayToStats(DayStatsData data)
    {
        LoadData();
        this.data.LastWeek.RemoveAt(0);
        this.data.LastWeek.Add(data);
        SaveData();
    }

    public List<DayStatsData> GetStats()
    {
        LoadData();
        return data.LastWeek;
    }
}
