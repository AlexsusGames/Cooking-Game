using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceDataProvider 
{
    private const string Key = "DeviceDataKey";
    private DeviceData data;

    Dictionary<string, Vector3> map;

    public DeviceDataProvider() => LoadData();

    public void SaveData()
    {
        data = new();

        foreach (var item in map.Keys)
        {
            data.DeviceNames.Add(item);
            data.Positions.Add(map[item]);
        }

        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(Key, save);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(Key))
        {
            string save = PlayerPrefs.GetString(Key);
            data = JsonUtility.FromJson<DeviceData>(save);
        }
        else data = new();

        CreateMap();
    }
    private void CreateMap()
    {
        map = new();

        for (int i = 0; i < data.DeviceNames.Count; i++)
        {
            map[data.DeviceNames[i]] = data.Positions[i];
        }
    }

    public void AddDevice(string name, Vector3 position)
    {
        map[name] = position;
    }

    public void RemovedDevice(string name)
    {
        if (!map.ContainsKey(name))
            return;

        map.Remove(name);
    }

    public bool Has(string name) => map.ContainsKey(name);
    public Vector3 GetPosition(string name) => map[name];
}
