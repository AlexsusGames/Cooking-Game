using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState 
{
    private const string Key = "World_State_Data_Key";

    public void SaveState(WorldStateData data)
    {
        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(Key, save);
    }

    public WorldStateData LoadState()
    {
        WorldStateData data;

        if (PlayerPrefs.HasKey(Key))
        {
            string save = PlayerPrefs.GetString(Key);
            data = JsonUtility.FromJson<WorldStateData>(save);
        }
        else data = new WorldStateData();

        return data;
    }
}

public class WorldStateData
{
    public bool KitchenLight;
    public bool BedroomLight;
    public int DishCount;
    public int RemainedFood;
    public int RemainedDrinks;
}
