using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryObjects;
    private List<IInventoryData> allInventories = new();

    private void Awake()
    {
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            inventoryObjects[i].TryGetComponent(out IInventoryData data);
            allInventories.Add(data);
        }

        LoadInventories();
    }

    private void OnDisable()
    {
        SaveInventories();
    }

    private void LoadInventories()
    {
        for (int i = 0; i < allInventories.Count; i++)
        {
            allInventories[i].Load();
        }
    }

    private void SaveInventories()
    {
        for (int i = 0; i < allInventories.Count; i++)
        {
            allInventories[i].Save();
        }
    }
}
