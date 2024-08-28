using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataLoader 
{
    private InventoryGridData data;

    private InventoryTypesConfig config = new();

    private void LoadData(InventoryTypes type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
        {
            var save = PlayerPrefs.GetString(type.ToString());
            data = JsonUtility.FromJson<InventoryGridData>(save);
        }
        else CreateData(type);
    }

    private void SaveData(InventoryTypes type)
    {
        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(type.ToString(), save);
    }

    public InventoryGridData GetInventory(InventoryTypes type)
    {
        LoadData(type);

        return data;
    }

    public void SaveInventory(InventoryGridData data, InventoryTypes type)
    {
        this.data = data;
        SaveData(type);
    }

    public void RemoveData(InventoryTypes type)
    {
        PlayerPrefs.DeleteKey(type.ToString());
    }

    public void CreateData(InventoryTypes type)
    {
        data = new InventoryGridData()
        {
            Size = config.GetInventorySizes(type),
            Slots = new()
        };

        var slotsAmount = data.Size.x * data.Size.y;
        for (int i = 0; i < slotsAmount; i++)
        {
            data.Slots.Add(new InventorySlotData());
        }
        SaveData(type);
    }

}
