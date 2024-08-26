using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverInventory 
{
    private InventoryGrid inventoryGrid;
    private InventoryDataLoader inventoryDataLoader = new();

    public DeliverInventory()
    {
        var inventoryData = inventoryDataLoader.GetInventory(InventoryTypes.CarTrunk);
        inventoryGrid = new InventoryGrid(inventoryData);
    }

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
}
