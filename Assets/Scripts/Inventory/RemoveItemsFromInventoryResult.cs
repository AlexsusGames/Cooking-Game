using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RemoveItemsFromInventoryResult 
{
    public readonly string InventoryId;
    public readonly int ItemsToRemoveAmount;
    public readonly int RemovedItemsAmount;

    public int NotRemovedItemsAmount => ItemsToRemoveAmount - RemovedItemsAmount;

    public RemoveItemsFromInventoryResult(string inventoryId, int itemsToRemove, int removedItems)
    {
        InventoryId = inventoryId;
        ItemsToRemoveAmount = itemsToRemove;
        RemovedItemsAmount = removedItems;
    }
}
