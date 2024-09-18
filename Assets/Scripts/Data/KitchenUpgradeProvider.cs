using System;
using System.Collections.Generic;
using UnityEngine;

public class KitchenUpgradeProvider
{
    private const string Key = "Kitchen_Upgrades_Key";
    private KitchenUpgradeData data;
    private Dictionary<InteractivePlaces, int> levelMap = new();

    public event Action<InteractivePlaces, int> OnUpgraded;

    public KitchenUpgradeProvider() => LoadData();

    public void SaveData()
    {
        Print();
        for (int i = 0; i < levelMap.Count; i++)
        {
            data.Upgrades[i].Level = levelMap[data.Upgrades[i].Type];
        }
        Print();

        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(Key, save);
    }

    public void Print()
    {
        string msg = "Map: ";

        foreach (var item in data.Upgrades)
        {
            msg += item.Type;
            msg += ", ";
        }

        Debug.Log(msg);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(Key))
        {
            string save = PlayerPrefs.GetString(Key);
            data = JsonUtility.FromJson<KitchenUpgradeData>(save);

        }
        else CreateData();

        CreateMap();
    }
    private void CreateData()
    {
        data = new KitchenUpgradeData();
        var types = Enum.GetValues(typeof(InteractivePlaces));

        for (int i = 0; i < types.Length; i++)
        {
            var type = (InteractivePlaces)types.GetValue(i);
            data.Upgrades.Add(new UpgradeData(type));
        }
    }

    private void CreateMap()
    {
        for (int i = 0; i < data.Upgrades.Count; i++)
        {
            levelMap[data.Upgrades[i].Type] = data.Upgrades[i].Level;
        }
    }

    public Dictionary<InteractivePlaces, int> GetUpgrades() => levelMap;

    public void ChangeLevel(InteractivePlaces type)
    {
        levelMap[type]++;
        OnUpgraded?.Invoke(type, levelMap[type]);
    }
}
