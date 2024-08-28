using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverInventory : MonoBehaviour, IInventoryData
{
    private const InventoryTypes TYPE = InventoryTypes.CarTrunk;
    private InventoryGrid inventoryGrid;
    private InventoryDataLoader inventoryDataLoader = new();
    private InventoryGridData data;

    public void AddItemToDeliver(string name, int amount)
    {
        inventoryGrid.AddItems(name, amount);
    }

    public (string name, int amount) TakeFirstItem()
    {
        var removedItem = inventoryGrid.RemoveFirstItem();

        if (!string.IsNullOrEmpty(removedItem.item) || removedItem.result.RemovedItemsAmount != 0)
        {
            return (removedItem.item, removedItem.result.RemovedItemsAmount);
        }

        return (null, 0);
    }


    public bool HasAnyProduct()
    {
        var products = inventoryGrid.GetInventoryItems();
        return products.Count > 0;
    }

    public void RemoveAllProducts()
    {
        inventoryDataLoader.RemoveData(TYPE);
    }

    public void Load()
    {
        data = inventoryDataLoader.GetInventory(TYPE);
        inventoryGrid = new InventoryGrid(data);
    }

    public void Save()
    {
        inventoryDataLoader.SaveInventory(data, TYPE);
    }
}
