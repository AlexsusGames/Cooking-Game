using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private List<GameObject> inventoryObjects;

    private readonly List<IInventoryData> allInventories = new();

    public void Add(GameObject inventoryObject)
    {
        inventoryObjects.Add(inventoryObject);
    }

    public void Load()
    {
        for (int i = 0; i < inventoryObjects.Count; i++)
        {
            inventoryObjects[i].TryGetComponent(out IInventoryData data);
            data.Load();
            allInventories.Add(data);
        }
    }

    public void Save()
    {
        for (int i = 0; i < allInventories.Count; i++)
        {
            allInventories[i].Save();
        }
    }
}
